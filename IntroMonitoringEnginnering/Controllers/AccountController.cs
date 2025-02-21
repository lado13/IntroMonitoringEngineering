using IntroMonitoringEngineering.Interfaces;
using IntroMonitoringEngineering.ViewModel.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntroMonitoringEngineering.Controllers
{
    public class AccountController : Controller
    {


        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.RegisterAsync(model);
                    TempData["SuccessMessage"] = "Registration successful!";
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(model);
                if (result)
                {
                    return RedirectToAction("Index","Context");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(new EditUserVM
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Username = user.UserName
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM editUserVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userService.UpdateUserAsync(editUserVM);
                    if (result)
                    {
                        TempData["SuccessMessage"] = "User updated successfully!";
                        return RedirectToAction("Dashboard");
                    }
                    ModelState.AddModelError(string.Empty, "User update failed.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(editUserVM);
        }



        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "User deleted successfully!";
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Dashboard");
        }


        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userService.GetUserByIdAsync(User.Identity.Name); 
            if (user == null)
            {
                return NotFound();
            }
            return View(new UserProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Username = user.UserName
            });
        }


        public IActionResult LogOut()
        {
            _userService.LogoutAsync();
            return RedirectToAction("Index","Context");
        }




    }
}
