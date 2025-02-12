using AssetsPro.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsPro.ViewModel
{
    public class AddUserViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [ForeignKey(nameof(Role))]
        public string RoleId { get; set; }

        public virtual IdentityRole? Role { get; set; }
    }
}
