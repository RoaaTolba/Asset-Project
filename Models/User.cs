using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsPro.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [ForeignKey(nameof(Group))]
        [Display(Name = nameof(Group))]
        public int Group_id { get; set; }
        public virtual Group? Group { get; set; }
    }
}
