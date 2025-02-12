using AssetsPro.NewDataAnnotation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsPro.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required...")]
        public string DateTime { get; set; } // Store as string in MM-dd-yyyy format

        [Required(ErrorMessage = "StartTime is required...")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required...")]
        public string EndTime { get; set; }

        [ForeignKey(nameof(Employee))]
        [Display(Name = "Employee")]
        public int Emp_Id { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
