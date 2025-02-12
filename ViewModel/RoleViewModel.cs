using System.ComponentModel.DataAnnotations;

namespace AssetsPro.ViewModel
{
    public class RoleViewModel
    {
        [Required, MaxLength(255)]
        public string RoleName { get; set; }
        public List<CheckboxViewModel> RoleClaims { get; set; }
    }
}
