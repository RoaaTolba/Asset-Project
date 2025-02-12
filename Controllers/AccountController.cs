using AssetsPro.Models;
using AssetsPro.Services;
using AssetsPro.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetsPro.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        //private readonly GroupService groupService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            //this.groupService = groupService;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> Login(LoginViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = await userManager.FindByNameAsync(userVM.UserName);
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(userModel, userVM.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "User name or password is wrong");
            }
            return View(userVM);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
