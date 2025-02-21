using IntroMonitoringEngineering.Models;
using IntroMonitoringEngineering.ViewModel.Account;
using Microsoft.AspNetCore.Identity;

namespace IntroMonitoringEngineering.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterVM model);
        Task<bool> LoginAsync(LoginVM model);
        Task<bool> LogoutAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<bool> UpdateUserAsync(EditUserVM editUserVM);
        Task<bool> DeleteUserAsync(string id);

    }
}
