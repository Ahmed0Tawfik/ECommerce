using ECommerce.Model.ViewModels;
using FirstMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _RoleManager;
        public RoleController(RoleManager<IdentityRole> RoleManager)
        {
            _RoleManager = RoleManager;
        }
        public IActionResult AddRole()
        {
            return View("AddRole");
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = model.RoleName;

                IdentityResult result = await _RoleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    ViewBag.Success = true;
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View("AddRole", model);
        }
    }
}
