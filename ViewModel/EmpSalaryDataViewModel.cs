using System.ComponentModel.DataAnnotations;

namespace AssetsPro.ViewModel
{
    public class EmpSalaryDataViewModel
    {
        [Key]
        public int Id { get; set; }
        public int Emp_Id { get; set; }
        public int Salary { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
