using AssetsPro.Constants;
using AssetsPro.Models;
using AssetsPro.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AssetsPro.Controllers
{
    //[Authorize(Permessions.Groups.Show)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [Authorize(Permessions.Groups.Show)]
        public async Task<IActionResult> Index()
        {
            ViewBag.roles = await roleManager.Roles.ToListAsync();
            return View();
        }
        [HttpGet]
        [Authorize(Permessions.Groups.Add)]
        public IActionResult AddRole()
        {
            var allPermissions = Permessions.GenerateAllPermissions()
                                .Select(c => new CheckboxViewModel { DisplayVlue = c })
                                .ToList();
            var viewModel = new RoleViewModel
            {
                RoleClaims = allPermissions
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (await roleManager.RoleExistsAsync(model.RoleName))
            {
                ModelState.AddModelError("Name", "Role is exist!");
                return View(model);
            }
            if (!ModelState.IsValid)
                return View(model);
            var role = new IdentityRole(model.RoleName.Trim());
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                var selectedClaims = model.RoleClaims.Where(c => c.IsSelected).ToList();
                foreach (var claim in selectedClaims)
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", claim.DisplayVlue));
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Permessions.Groups.Edit)]
        public async Task<IActionResult> Edit(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if(role == null) 
                return NotFound();
            var roleClaims = roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();
            var allPermissions= Permessions.GenerateAllPermissions().Select(c => new CheckboxViewModel { DisplayVlue=c}).ToList();
            foreach (var permission in allPermissions)
            {
                if(roleClaims.Any(c => c == permission.DisplayVlue))
                    permission.IsSelected = true;

            }
            var viewModel = new PermissionFormViewModel
            {
                RoleId = roleId,
                RoleName = role.Name,
                RoleClaims = allPermissions
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PermissionFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var role = await roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
                return NotFound();

            if (role.Name != model.RoleName)
            {
                role.Name = model.RoleName;
                var updateResult = await roleManager.UpdateAsync(role);
                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }

            var roleClaims = await roleManager.GetClaimsAsync(role);
            foreach (var claim in roleClaims)
            {
                await roleManager.RemoveClaimAsync(role,claim);
            }
            var selectedClaims = model.RoleClaims.Where(c => c.IsSelected).ToList();
            foreach (var claim in selectedClaims)
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission",claim.DisplayVlue));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}