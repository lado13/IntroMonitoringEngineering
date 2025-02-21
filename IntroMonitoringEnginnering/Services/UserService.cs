using IntroMonitoringEngineering.Interfaces;
using IntroMonitoringEngineering.Models;
using IntroMonitoringEngineering.Role;
using IntroMonitoringEngineering.ViewModel.Account;
using Microsoft.AspNetCore.Identity;

namespace IntroMonitoringEngineering.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<User> RegisterAsync(RegisterVM model)
        {
            try
            {
                // Check if the user with the given email already exists
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    throw new Exception("A user with this email already exists.");
                }

                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FullName = model.FullName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"User creation failed: {errors}");
                }

                var roleResult = await _userManager.AddToRoleAsync(user, ApplicationRoles.User);

                if (!roleResult.Succeeded)
                {
                    var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    throw new Exception($"Role assignment failed: {roleErrors}");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during registration: {ex.Message}", ex);
            }
        }




        public async Task<bool> LoginAsync(LoginVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }

            return false;
        }

        public async Task<bool> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new Exception("User not found.");
            return user;
        }

        public async Task<bool> UpdateUserAsync(EditUserVM editUserVM)
        {
            var user = await _userManager.FindByIdAsync(editUserVM.Id);

            if (user == null) throw new Exception("User not found.");

            user.FullName = editUserVM.FullName;
            user.Email = editUserVM.Email;
            user.UserName = editUserVM.Username;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) throw new Exception("User not found.");

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }


    }
}
