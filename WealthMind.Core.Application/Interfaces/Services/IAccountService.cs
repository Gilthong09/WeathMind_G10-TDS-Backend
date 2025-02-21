using WealthMind.Core.Application.DTOs.Account;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<AuthenticationResponse> AuthenticateWebApiAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request);
        Task<GenericResponse> UpdateUserStatusAsync(string userId);
        Task<GenericResponse> ChangeUserStatus(string userId, bool status);
        Task<GenericResponse> DeleteUserAsync(string userId);
        Task<UserDTO> FindByEmailAsync(string email);
        Task<UserDTO> FindByIdAsync(string id);
        Task SingOutAsync();
        Task<List<UserDTO>> GetAllAdminAsync();
        Task<List<UserDTO>> GetAllDeveloperAsync();
    }
}