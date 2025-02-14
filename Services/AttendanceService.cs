using AssetsPro.Interfaces;
using AssetsPro.Models;
using AssetsPro.Repos;
using AssetsPro.ViewModel;

namespace AssetsPro.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepo attendanceRepo;

        public AttendanceService(IAttendanceRepo attendanceRepo)
        {
            this.attendanceRepo = attendanceRepo;
        }
        public void DeleteById(int id) => attendanceRepo.DeleteById(id);
        public void Edit(int id, Attendance newAttendance)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Attendance> GetAll()
        {
            return attendanceRepo.GetAll();
        }
        public Attendance GetById(int id) => attendanceRepo.GetById(id);
        public IEnumerable<Employee> GetEmpNames()
        {
            return attendanceRepo.GetEmpNames();
        }
        public async Task<bool> SaveRecords(List<Attendance> Records)
        {
            if (Records == null || !Records.Any())
            {
                return false; // Handle empty or null input gracefully
            }

            try
            {
                return await attendanceRepo.SaveRecords(Records);
            }
            catch (Exception)
            {
                // Log the exception if needed
                return false; // Return false if any error occurs
            }

        }
        public List<EmpSalaryDataViewModel> GetEmpSalaryData()
        {
            return attendanceRepo.GetEmpSalaryData();
        }
        public Task<bool> isExistINmonth(int empId)
        {
            return attendanceRepo.isExistINmonth(empId);
        }
        public void insertSalary(SalaryReport salaryR)
        {
            attendanceRepo.insertSalary(salaryR);
        }
        public void updateSalary(SalaryReport salaryR)
        {
            attendanceRepo.updateSalary(salaryR);
        }
        public List<SalaryReportViewModel> salaryReportData()
        {
            return attendanceRepo.salaryReportData();
        }
        public SalaryReportViewModel findSalaryById(int id)
        {
            return attendanceRepo.findSalaryById(id);
        }
    }
}
