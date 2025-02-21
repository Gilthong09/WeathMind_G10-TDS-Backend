using AutoMapper;
using WealthMind.Core.Application.DTOs.Account;
using WealthMind.Core.Application.Enums;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.User;

namespace WealthMind.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        #region Login & Logout
        public async Task SignOutAsync()
        {
            await _accountService.SingOutAsync();
        }

        #endregion

        #region Email Confirmation
        /// <summary>
        /// Confirms the email of a user asynchronously.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="token">The confirmation token.</param>
        /// <returns>The confirmation result.</returns>
        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        #endregion

        #region Register
        /// <summary>
        /// Registers a user asynchronously.
        /// </summary>
        /// <param name="vm">The view model containing user information.</param>
        /// <param name="origin">The origin of the registration request.</param>
        /// <returns>The registration response.</returns>
        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            registerRequest.Role = (int)Roles.User;

            return await _accountService.RegisterUserAsync(registerRequest, origin);
        }
        #endregion

        #region Get Methods

        #region GetByEmail
        /// <summary>
        /// Retrieves a user by email asynchronously.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user view model.</returns>
        public async Task<UserViewModel> GetByEmailAsync(string email)
        {
            UserDTO userDTO = await _accountService.FindByEmailAsync(email);

            UserViewModel vm = _mapper.Map<UserViewModel>(userDTO);

            return vm;
        }
        #endregion

        #region GetById
        /// <summary>
        /// Retrieves a user by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user view model.</returns>
        public async Task<UserViewModel> GetByIdAsync(string id)
        {
            UserDTO userDTO = await _accountService.FindByIdAsync(id);

            UserViewModel vm = _mapper.Map<UserViewModel>(userDTO);

            return vm;

        }
        #endregion

        #endregion

        #region Update
        /// <summary>
        /// Updates a user asynchronously.
        /// </summary>
        /// <param name="vm">The view model containing user information.</param>
        /// <returns>The update response.</returns>
        public async Task<UpdateUserResponse> UpdateUserAsync(SaveUserViewModel vm)
        {
            UpdateUserRequest updateRequest = _mapper.Map<UpdateUserRequest>(vm);

            return await _accountService.UpdateUserAsync(updateRequest);
        }
        #endregion

        #region Delete
        public async Task<GenericResponse> DeleteUserAsync(string userId)
        {
            var response = await _accountService.DeleteUserAsync(userId);
            return response;

        }
        #endregion

    }
}
