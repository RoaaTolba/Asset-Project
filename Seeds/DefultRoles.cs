//using Microsoft.AspNet.Identity;
using AssetsPro.Constants;
using Microsoft.AspNetCore.Identity;

namespace AssetsPro.Seeds
{
    public static class DefultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.BasicUser.ToString()));
            }
        }
    }
}
