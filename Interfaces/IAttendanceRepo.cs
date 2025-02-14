using AssetsPro.Models;
using AssetsPro.Repos;
using AssetsPro.ViewModel;

namespace AssetsPro.Interfaces
{
    public interface IAttendanceRepo
    {
        public IEnumerable<Attendance> GetAll();
        public IEnumerable<Employee> GetEmpNames();
        public Attendance GetById(int id);
        public Task<bool> SaveRecords(List<Attendance> records);
        public void Edit (int id,Attendance newAttendance);
        public void DeleteById(int id);
        public List<EmpSalaryDataViewModel> GetEmpSalaryData();
        public Task<bool> isExistINmonth(int empId);
        public void insertSalary(SalaryReport salaryR);
        public void updateSalary(SalaryReport salaryR);
        public List<SalaryReportViewModel> salaryReportData();
        public SalaryReportViewModel findSalaryById(int id);
    }
}
