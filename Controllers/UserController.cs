using AssetsPro.Constants;
using AssetsPro.Interfaces;
using AssetsPro.Models;
using AssetsPro.Services;
using AssetsPro.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Versioning;

namespace AssetsPro.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        //private readonly GroupService groupService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            //this.groupService = groupService;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [Authorize(Permessions.Users.Show)]
        public async Task<IActionResult> ShowUsers()
        {
            var list = await userManager.Users.ToListAsync();
            var RUlist = new List<AppUserViewModel>();
            foreach(var user in list)
            {
                //user doesn't id
                var roles = await userManager.GetRolesAsync(user);
                if (!roles.Any())
                    continue;

                //return BadRequest($"User {user.UserName} has no roles assigned.");
                var roleName = roles.FirstOrDefault() ?? "No Role Assigned";
                //var roleName = roles[0]; // Assuming the user has exactly one role
                //var role = await roleManager.FindByNameAsync(roleName);
                //if (role == null)
                //{
                //    return NotFound("Role not found.");
                //}
                var userViewModel = new AppUserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    UserName = user.UserName,
                    Email = user.Email,
                    RoleName = roleName // Assign the role ID
                };

                // Add the mapped user to the list
                RUlist.Add(userViewModel);
            }
            return View(RUlist);
        }
        [HttpGet]
        [Authorize(Permessions.Users.Add)]
        public async Task<IActionResult> AddUsers()
        {
            ViewData["RoleList"] = await roleManager.Roles.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUsers(AddUserViewModel UserVM)
        {
            if (UserVM != null)
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser UserModel = new ApplicationUser();
                    UserModel.Email = UserVM.Email;
                    UserModel.UserName = UserVM.UserName;
                    UserModel.PasswordHash = UserVM.Password;
                    UserModel.Name = UserVM.Name;
                    IdentityResult result = await userManager.CreateAsync(UserModel, UserVM.Password);
                    if (result.Succeeded)
                    {
                        //create cookie?
                        //await signInManager.SignInAsync(UserModel, false);
                        var role = await roleManager.FindByIdAsync(UserVM.RoleId);
                        if (role != null)
                            await userManager.AddToRoleAsync(UserModel, role.Name);
                        TempData["Message"] = "User added successfully. They will need to log in to access the system.";
                        return RedirectToAction("ShowUsers");
                    }
                    else
                    {
                        foreach (var errorItem in result.Errors)
                        {
                            ModelState.AddModelError("Password", errorItem.Description);
                        }
                    }
                }
            }
            ViewData["RoleList"] = await roleManager.Roles.ToListAsync();
            return View(UserVM);
        }
        [HttpGet]
        [Authorize(Permessions.Users.Edit)]
        public async Task<IActionResult> Edit(string id)
        {
            ViewData["RoleList"] = await roleManager.Roles.ToListAsync();
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var Role = await userManager.GetRolesAsync(user);
            if (Role.Count == 0)
                return NotFound("User has no assigned role.");
            var roleName = Role[0]; // Assuming the user has exactly one role
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound("Role not found.");
            }
            // Map ApplicationUser to AddUserViewModel
            var userVM = new EditUserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                RoleId= role.Id
            };

            return View(userVM);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditUserViewModel UserVM)
        {
            if (ModelState.IsValid)
            {
                ViewData["RoleList"] = await roleManager.Roles.ToListAsync();
                var user = await userManager.FindByIdAsync(id.ToString());
                if (user==null) return NotFound();
                user.Email = UserVM.Email;
                user.UserName = UserVM.UserName;
                user.Name = UserVM.Name;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var currentRole = await userManager.GetRolesAsync(user);
                    if (currentRole.Any())
                    {
                        await userManager.RemoveFromRolesAsync(user, currentRole);
                    }
                    var newRole = await roleManager.FindByIdAsync(UserVM.RoleId);
                    if (newRole != null)
                    {
                        await userManager.AddToRoleAsync(user, newRole.Name);
                    }
                    return RedirectToAction("ShowUsers");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View("Edit", UserVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Permessions.Users.Delete)]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                TempData["AlertMessage"] = "Employee not found.";
                return RedirectToAction("ShowUsers");
            }

            // Attempt to delete the user
            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["Message"] = "Employee deleted successfully.";
                return RedirectToAction("ShowUsers");
            }

            // If deletion failed, add errors to TempData
            TempData["AlertMessage"] = "Employee could not be deleted.";
            return RedirectToAction("Index");
        }
    }
}
