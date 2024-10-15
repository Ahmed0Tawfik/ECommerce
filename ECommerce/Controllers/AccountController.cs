using ECommerce.Model.Models;
using ECommerce.Model.ViewModels;
using FirstMVC.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SigninManager;

        public AccountController(UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> SigninManager)
        {
            _SigninManager = SigninManager;
            _UserManager = UserManager;
        }

        public async Task<IActionResult> SignOut()
        {
            await _SigninManager.SignOutAsync();
            TempData["Logout"] = "You have been logged out";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View("Login");
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult>SaveLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser =
                await _UserManager.FindByEmailAsync(model.Email);
                if (applicationUser != null)
                {
                    bool found =
                   await _UserManager.CheckPasswordAsync(applicationUser, model.Password);
                    if (found)
                    {

                        var Claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,applicationUser.UserName),
                        };

                        var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await _SigninManager.SignInAsync(applicationUser, model.RememberMe, identity.ToString());
                        TempData["Login"] = "You have been logged in";
                        return RedirectToAction("Index", "Home");
                    }

                }
                ModelState.AddModelError("", " Wrong Username Or Password");

            }
            TempData["Logout"] = "Wrong Username Or Password";
            return View("Login", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.FirstName = model.FName;
                applicationUser.LastName = model.LName;
                applicationUser.UserName = model.UserName;
                applicationUser.Email = model.Email;
                applicationUser.PhoneNumber = model.PhoneNumber;
                applicationUser.PasswordHash = model.Password;

                IdentityResult result = await _UserManager.CreateAsync(applicationUser, model.Password);
                if (result.Succeeded)
                {
                    await _UserManager.AddToRoleAsync(applicationUser, "Customer");

                    await _SigninManager.SignInAsync(applicationUser, isPersistent: false);
                    TempData["Login"] = "Account Created";
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }
            else
            {
                TempData["Logout"] = "Invalid Info";
                return View("Register", model);
            }

            return View("Register", model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateAdmin()
        {
            return View("CreateAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> SaveAdmin(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.FirstName = model.FName;
                applicationUser.LastName = model.LName;
                applicationUser.UserName = model.UserName;
                applicationUser.Email = model.Email;
                applicationUser.PhoneNumber = model.PhoneNumber;
                applicationUser.PasswordHash = model.Password;

                IdentityResult result = await _UserManager.CreateAsync(applicationUser, model.Password);
                if (result.Succeeded)
                {
                    await _UserManager.AddToRoleAsync(applicationUser, "Admin");

                    await _SigninManager.SignInAsync(applicationUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }
            else
            {
                return View("CreateAdmin", model);
            }

            return View("CreateAdmin", model);
        }



        [Authorize]

        public  IActionResult Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

            var user =  _UserManager.Users
                                .Include(u => u.Orders)
                                .FirstOrDefault(u => u.Id == userId); // Get logged-in user ID
            UpdateAccountViewModel model = new UpdateAccountViewModel();
            model.user = user;
            model.FName = user.FirstName;
            model.LName = user.LastName;
            model.Email = user.Email;
            model.Phone = user.PhoneNumber;

            return View("Profile", model);
        }

        public IActionResult Update(UpdateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID
                ApplicationUser applicationUser = _UserManager.Users.FirstOrDefault(u => u.Id == userId);

                applicationUser.FirstName = model.FName;
                applicationUser.LastName = model.LName;
                applicationUser.Email = model.Email;
                applicationUser.PhoneNumber = model.Phone;
                IdentityResult result = _UserManager.UpdateAsync(applicationUser).Result;
                if (result.Succeeded)
                {
                    TempData["Login"] = "Account Updated";
                    return RedirectToAction("Profile");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Profile", model);
        }
    }
}
