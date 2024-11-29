using AssetsPro.NewDataAnnotation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsPro.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Date is required...")]
        [DataType(DataType.Date)]
        [DateRangeUntilToday("01-01-2000")]
        public DateTime DateTime { get; set; }
        [Required(ErrorMessage = "StartTime is required...")]
        public TimeSpan StartTime { get; set; }
        [Required(ErrorMessage = "EndTime is required...")]
        public TimeSpan? EndTime { get; set; }
        [ForeignKey(nameof(Employee))]
        [Display(Name ="Employee")]
        public int Emp_Id { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
