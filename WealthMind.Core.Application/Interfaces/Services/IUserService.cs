using WealthMind.Core.Application.DTOs.Account;
using WealthMind.Core.Application.ViewModels.User;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<UpdateUserResponse> UpdateUserAsync(SaveUserViewModel vm);
        Task<GenericResponse> DeleteUserAsync(string userId);
        Task<UserViewModel> GetByEmailAsync(string email);
        Task<UserViewModel> GetByIdAsync(string id);
        Task SignOutAsync();
    }
}