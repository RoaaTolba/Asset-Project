using AssetsPro.Interfaces;
using AssetsPro.Models;
using AssetsPro.Repos;
using AssetsPro.ViewModel;
using System.Globalization;

namespace AssetsPro.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepo attendanceRepo;
        private const double OVERTIME_RATE = 7;
        private const double DISCOUNT_RATE = 10;
        private const int ABSENT_DEDUCTION = 100;

        public AttendanceService(IAttendanceRepo attendanceRepo)
        {
            this.attendanceRepo = attendanceRepo;
        }

        // --------- Basic CRUD ----------
        public void DeleteById(int id) => attendanceRepo.DeleteById(id);

        public void Edit(int id, Attendance newAttendance)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Attendance>> GetAll() => attendanceRepo.GetAll();

        public Attendance GetById(int id) => attendanceRepo.GetById(id);

        public IEnumerable<Employee> GetEmpNames() => attendanceRepo.GetEmpNames();

        public async Task<bool> SaveRecords(List<Attendance> Records)
        {
            if (Records == null || !Records.Any())
                return false;

            try
            {
                return await attendanceRepo.SaveRecords(Records);
            }
            catch (Exception)
            {
                // Log exception if needed
                return false;
            }
        }

        public List<EmpSalaryDataViewModel> GetEmpSalaryData() => attendanceRepo.GetEmpSalaryData();

        public Task<bool> isExistINmonth(int empId) => attendanceRepo.isExistINmonth(empId);

        public void insertSalary(SalaryReport salaryR) => attendanceRepo.insertSalary(salaryR);

        public void updateSalary(SalaryReport salaryR) => attendanceRepo.updateSalary(salaryR);

        public List<SalaryReportViewModel> salaryReportData() => attendanceRepo.salaryReportData();

        public SalaryReportViewModel findSalaryById(int id) => attendanceRepo.findSalaryById(id);

        // --------- Salary Calculation Logic (Clean Code) ---------
        public async Task CalculateMonthlySalaryAsync(List<Attendance> records)
        {
            var employees = GetEmpSalaryData();

            foreach (var emp in employees)
            {
                var attendanceRecord = GetEmployeeAttendance(emp.Emp_Id, records);

                if (attendanceRecord == null)
                {
                    // الموظف غايب
                    await HandleAbsentAsync(emp);
                    continue;
                }

                // الموظف حضر → حساب التأخير/الإضافي
                var (overtimeHours, discountHours) = CalculateHours(emp, attendanceRecord);

                // بناء Salary Report
                var salaryReport = BuildSalaryReport(emp, overtimeHours, discountHours);

                // حفظ Salary في DB
                await SaveSalaryReportAsync(emp.Emp_Id, salaryReport);
            }
        }

        // -------- Helper Methods --------

        private Attendance GetEmployeeAttendance(int empId, List<Attendance> records)
        {
            return records.FirstOrDefault(r => r.Emp_Id == empId);
        }

        private (double overtimeHours, double discountHours) CalculateHours(EmpSalaryDataViewModel emp, Attendance record)
        {
            double overtime = 0, discount = 0;

            TimeSpan recStart = TimeSpan.ParseExact(record.StartTime, "hh\\:mm", CultureInfo.InvariantCulture);
            TimeSpan empStart = TimeSpan.ParseExact(emp.StartTime, "hh\\:mm", CultureInfo.InvariantCulture);
            double startDiff = (recStart - empStart).TotalHours;

            TimeSpan recEnd = TimeSpan.ParseExact(record.EndTime, "hh\\:mm", CultureInfo.InvariantCulture);
            TimeSpan empEnd = TimeSpan.ParseExact(emp.EndTime, "hh\\:mm", CultureInfo.InvariantCulture);
            double endDiff = (recEnd - empEnd).TotalHours;

            if (startDiff > 0) discount += startDiff;       // اتأخر
            else if (startDiff < 0) overtime += -startDiff; // حضر بدري

            if (endDiff < 0) discount += -endDiff;         // خرج بدري
            else if (endDiff > 0) overtime += endDiff;     // قعد زيادة

            return (overtime, discount);
        }

        private SalaryReport BuildSalaryReport(EmpSalaryDataViewModel emp, double overtimeHours, double discountHours)
        {
            double extra = overtimeHours * OVERTIME_RATE;
            double discount = discountHours * DISCOUNT_RATE;
            double total = emp.Salary + extra - discount;

            return new SalaryReport
            {
                Emp_Id = emp.Emp_Id,
                Attendance_days = 1,
                Absent_days = 0,
                Overtime_Hours = overtimeHours,
                Discount_Hours = discountHours,
                Extra = (int)extra,
                Discount = (int)discount,
                Total = (int)total
            };
        }

        private async Task HandleAbsentAsync(EmpSalaryDataViewModel emp)
        {
            bool exists = await isExistINmonth(emp.Emp_Id);

            var salaryReport = new SalaryReport
            {
                Emp_Id = emp.Emp_Id,
                Attendance_days = 0,
                Absent_days = 1,
                Overtime_Hours = 0,
                Discount_Hours = 0,
                Extra = 0,
                Discount = 0,
                Total = exists ? 0 : emp.Salary - ABSENT_DEDUCTION
            };

            if (exists) updateSalary(salaryReport);
            else insertSalary(salaryReport);
        }

        private async Task SaveSalaryReportAsync(int empId, SalaryReport salaryReport)
        {
            bool exists = await isExistINmonth(empId);

            if (exists)
                updateSalary(salaryReport);
            else
                insertSalary(salaryReport);
        }
    }
}
