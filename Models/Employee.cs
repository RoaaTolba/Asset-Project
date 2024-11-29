using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AssetsPro.NewDataAnnotation;
namespace AssetsPro.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3,ErrorMessage ="Minimum length is 3")]
        [MaxLength(100, ErrorMessage = "Maximum length is 100")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required]
        public int ContactNumber { get; set; }
        [Required]
        [DateRangeUntilToday("01-01-2000")]
        public DateTime BirthDate { get; set; }
        [Required]
        [Display(Name = "Gender")]
        [ForeignKey(nameof(Gender))]
        public int Gender_id { get; set; }
        [Required]
        [DateRangeUntilToday("01-01-2000")]
        public DateTime date_of_contract { get; set; }
        [Required(ErrorMessage = "Start time is required.")]
        public TimeOnly start_time { get; set; }
        [Required(ErrorMessage = "End time is required.")]
        public TimeOnly end_time { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{4}",ErrorMessage ="Just 4 numbers")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "National ID is required.")]
        public string SSN { get; set; }
        [Required(ErrorMessage = "Nationality is required.")]
        public string Nationality { get; set; }
        public string? Note { get; set; }
        public virtual Gender? Gender { get; set; }
    }
}