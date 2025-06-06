﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WealthMind.Core.Application.DTOs.Account;
using WealthMind.Core.Application.DTOs.Email;
using WealthMind.Core.Application.Enums;
using WealthMind.Core.Application.Exceptions;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Domain.Settings;
using WealthMind.Infrastructure.Identity.Entities;

namespace WealthMind.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JWTSettings> jwtSettings, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
        }

        #region Login

        #region AuthenticateApi
        public async Task<AuthenticationResponse> AuthenticateWebApiAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Credential);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.Credential);
                if (user == null)
                {
                    response.HasError = true;
                    response.Error = $"No accounts registered with email or username: {request.Credential}";
                    return response;
                }

            }


            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    response.HasError = true;
                    response.Error = $"Account locked due to too many failed attempts.";
                    return response;
                }
                response.HasError = true;
                response.Error = $"Invalid credentials, please try again.";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account not confirmed for {user.Email}";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            response.Id = user.Id;

            response.Email = user.Email;

            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();

            if (response.Roles.Any(role => role == "Agent" || role == "Client"))
            {
                throw new ApiException($"Account not authorize for this resource.", (int)HttpStatusCode.Forbidden);

                //response.HasError = true;
                //response.Error = $"Account not authorize for this resource.";
                //return response;
            }

            response.IsVerified = user.EmailConfirmed;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }
        #endregion

        #region SingOut
        public async Task SingOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        #endregion

        #endregion

        #region Register
        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}'is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                ProfilePicture = request.ProfilePicture,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                switch (request.Role)
                {
                    case (int)Roles.User:

                        await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                        // Here we send an email to the user to confirm the account
                        var verificationUri = await SendVerificationEmailUri(user, origin);
                        await _emailService.SendAsync(new EmailRequest()
                        {
                            To = user.Email,
                            Subject = "Confirm your registration at WealthMind.",
                            Body = $"Please confirm your account by visiting this URL: <a href=\"{verificationUri}\">link</a>.<br/>"
                        });
                        break;

                    case (int)Roles.Admin:

                        await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());

                        break;

                    case (int)Roles.Developer:

                        await _userManager.AddToRoleAsync(user, Roles.Developer.ToString());
                        break;

                    default:
                        response.HasError = true;
                        response.Error = $"An error has occurred trying to register the user.";
                        return response;
                }
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error has occurred trying to register the user.";
                return response;
            }

            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No accounts registered with this user.";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app.";
            }
            else
            {
                return $"An error occurred while trying to confirm the email: {user.Email}.";
            }
        }

        #endregion

        #region Update
        public async Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request)
        {
            UpdateUserResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null && userWithSameUserName.Id != request.Id)
            {
                response.HasError = true;
                response.Error = $"Username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null && userWithSameEmail.Id != request.Id)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}'is already registered.";
                return response;
            }

            var user = await _userManager.FindByIdAsync(request.Id);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.UserName = request.UserName;
            user.ProfilePicture = request?.ImageBase64 ?? user.ProfilePicture;


            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (request.Password != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    result = await _userManager.ResetPasswordAsync(user, token, request.Password);

                    if (!result.Succeeded)
                    {
                        response.HasError = true;
                        response.Error = $"An error ocurred while trying to update the password.";
                        return response;
                    }
                }
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error ocurred while trying to update the user.";
                return response;
            }

            return response;
        }

        #endregion

        #region Delete
        public async Task<GenericResponse> DeleteUserAsync(string Id)
        {
            GenericResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"User not found.";
                return response;
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error has ocurred trying to delete the user.";
                return response;
            }

            return response;
        }
        #endregion

        #region Active & Unactive 
        public async Task<GenericResponse> UpdateUserStatusAsync(string username)
        {
            GenericResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"User: {username} not found.";
                return response;
            }

            user.EmailConfirmed = !user.EmailConfirmed;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error has ocurred trying to update the status of the user: {username}.";
                return response;
            }

            return response;
        }

        public async Task<GenericResponse> ChangeUserStatus(string userId, bool status)
        {
            GenericResponse response = new()
            {
                HasError = false
            };
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"User not found.";
                return response;
            }
            user.EmailConfirmed = status;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error has ocurred trying to update the status of the user.";
                return response;
            }

            return response;
        }
        #endregion

        #region Finders
        public async Task<UserDTO> FindByEmailAsync(string email)
        {
            UserDTO userDTO = new();

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                userDTO.Id = user.Id;
                userDTO.UserName = user.UserName;
                userDTO.FirstName = user.FirstName;
                userDTO.LastName = user.LastName;
                userDTO.Email = user.Email;
                userDTO.EmailConfirmed = user.EmailConfirmed;
                userDTO.ProfilePicture = user.ProfilePicture;
                userDTO.Role = (await _userManager.GetRolesAsync(user).ConfigureAwait(false)).ToList()[0];

                return userDTO;
            }

            return null;

        }

        public async Task<UserDTO> FindByIdAsync(string Id)
        {
            UserDTO userDTO = new();

            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                userDTO.Id = user.Id;
                userDTO.UserName = user.UserName;
                userDTO.FirstName = user.FirstName;
                userDTO.LastName = user.LastName;
                userDTO.Email = user.Email;
                userDTO.ProfilePicture = user.ProfilePicture;
                userDTO.Role = (await _userManager.GetRolesAsync(user).ConfigureAwait(false)).ToList()[0];
                userDTO.EmailConfirmed = user.EmailConfirmed;

                return userDTO;
            }

            return null;

        }

        #endregion

        #region GetAllUserAsync
        public async Task<List<UserDTO>> GetAllAdminAsync()
        {
            var userDTOList = await GetAllUserAsync();
            userDTOList = userDTOList.Where(user => user.Role == Roles.Admin.ToString()).ToList();

            return userDTOList;
        }

        public async Task<List<UserDTO>> GetAllDeveloperAsync()
        {
            var userDTOList = await GetAllUserAsync();
            userDTOList = userDTOList.Where(user => user.Role == Roles.Developer.ToString()).ToList();

            return userDTOList;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var userDTOList = await GetAllUserAsync();
            userDTOList = userDTOList.Where(user => user.Role == Roles.User.ToString()).ToList();

            return userDTOList;
        }
        #endregion

        #region Password Reset
        public async Task<GenericResponse> RequestPasswordResetAsync(string email, string origin)
        {
            GenericResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered with the email: {email}";
                return response;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var resetLink = $"{origin}/reset-password?token={encodedToken}&email={WebUtility.UrlEncode(email)}";
            
            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Subject = "Reset Password at WealthMind.",
                Body = $"To reset your password, please click here: <a href=\"{resetLink}\">link</a>.<br/>" +
                       $"If you did not request this, please ignore this email."
            });

            return response;
        }

        public async Task<GenericResponse> ResetPasswordAsync(string token, string email, string newPassword)
        {
            GenericResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered with the email: {email}";
                return response;
            }

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Error occurred while trying to reset the password: {string.Join(", ", result.Errors.Select(e => e.Description))}";
                return response;
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
                await _userManager.ResetAccessFailedCountAsync(user);
            }

            return response;
        }
        #endregion

        #region Private Methods

        #region JWT Methods
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
                new Claim(JwtRegisteredClaimNames.MiddleName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);



            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
        #endregion

        #region Email Methods

        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }

        #endregion

        #region GetAllUserAsync
        private async Task<List<UserDTO>> GetAllUserAsync()
        {
            var userList = await _userManager.Users.ToListAsync();

            var userDTOList = userList.Select(user => new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                ProfilePicture = user.ProfilePicture


            }).ToList();

            foreach (var userDTO in userDTOList)
            {
                var user = await _userManager.FindByIdAsync(userDTO.Id);
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                userDTO.Role = rolesList.ToList()[0];
            }

            return userDTOList;
        }
        #endregion

        #endregion
    }
}
