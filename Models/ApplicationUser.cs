using Microsoft.AspNetCore.Identity;

namespace AssetsPro.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
