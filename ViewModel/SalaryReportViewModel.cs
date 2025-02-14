using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AssetsPro.Models;

namespace AssetsPro.ViewModel
{
    public class SalaryReportViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Phone { get; set; }
        public int Attendance_days { get; set; }
        public int Absent_days { get; set; }
        public double Overtime_Hours { get; set; }
        public double Discount_Hours { get; set; }
        public int Extra { get; set; }
        public int Discount { get; set; }
        public int Total { get; set; }
    }
}
