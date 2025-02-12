using AssetsPro.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsPro.ViewModel
{
    public class AppUserViewModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [ForeignKey(nameof(Role))]
        public string RoleName { get; set; }

        public virtual IdentityRole? Role { get; set; }
    }
}
