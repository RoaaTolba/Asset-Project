using AssetsPro.Interfaces;
using AssetsPro.Models;
using AssetsPro.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AssetsPro.Repos
{
    public class AttendanceRepo : IAttendanceRepo
    {
        MyDbContext context = new MyDbContext();
        public void DeleteById(int id) => context.Attendances.Remove(GetById(id));
        public void Edit(int id, Attendance newAttendance)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Attendance>> GetAll()
        {
            return await context.Attendances.ToListAsync();
        }

        public Attendance GetById(int id) => context.Attendances.FirstOrDefault(x => x.Id == id);
        public IEnumerable<Employee> GetEmpNames()
        {
            return context.Employees.ToList();
        }
        public async Task<bool> SaveRecords(List<Attendance> records)
        {
            try
            {
                foreach (var record in records)
                {
                    // Ensure record validity before adding
                    if (record != null)
                    {
                        context.Attendances.Add(record);
                    }
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log the exception if necessary
                return false; // Return false if an error occurs while saving
            }
        }
        public List<EmpSalaryDataViewModel> GetEmpSalaryData()
        {
            var empSalaryData = context.Employees
                .Select(employee => new EmpSalaryDataViewModel // Project into the ViewModel
                {
                    Id = 0, // Assuming Id is not needed or can be set to 0
                    Emp_Id = employee.Id,
                    Salary = employee.Salary, // Get Salary from Employee table
                    StartTime = employee.start_time.ToString(@"hh\:mm"), // Format start_time
                    EndTime = employee.end_time.ToString(@"hh\:mm") // Format end_time
                })
                .ToList();

            return empSalaryData;
        }
        public List<SalaryReportViewModel> salaryReportData()
        {
            var SalaryData = context.Salaries
                .Join(
                    context.Employees,
                    salary => salary.Emp_Id,
                    emp => emp.Id,
                    (salary, emp) => new SalaryReportViewModel
                    {
                        Id = salary.Id,
                        Name = emp.Name,
                        Salary = emp.Salary,
                        Phone = emp.ContactNumber.ToString(),
                        Attendance_days = salary.Attendance_days,
                        Absent_days = salary.Absent_days,
                        Overtime_Hours = salary.Overtime_Hours,
                        Discount_Hours = salary.Discount_Hours,
                        Extra = salary.Extra,
                        Discount = salary.Discount,
                        Total = salary.Total
                    }
                ).ToList();
            return SalaryData;
        }
        public async Task<bool> isExistINmonth(int empId)
        {
            bool isExist = await context.Salaries
                                    .AnyAsync(sr => sr.Emp_Id == empId &&
                                      sr.CreatedAt.Month == DateTime.Now.Month &&
                                      sr.CreatedAt.Year == DateTime.Now.Year);
            return isExist;
        }
        public void insertSalary(SalaryReport salaryR)
        {
            context.Salaries.Add(salaryR);
            context.SaveChanges();
        }
        public void updateSalary(SalaryReport salaryR)
        {
            SalaryReport updated = context.Salaries.FirstOrDefault(x => x.Emp_Id == salaryR.Emp_Id);
            if (updated != null)
            {
                updated.Attendance_days += salaryR.Attendance_days;
                updated.Absent_days += salaryR.Absent_days;
                updated.Overtime_Hours += salaryR.Overtime_Hours;
                updated.Discount_Hours += salaryR.Discount_Hours;
                updated.Extra += salaryR.Extra;
                updated.Discount += salaryR.Discount;
                updated.Total += salaryR.Total;
                context.SaveChanges();
            }
        }
        public SalaryReportViewModel findSalaryById(int id) 
        {
            var salaryData = context.Salaries
                    .Join(
                        context.Employees,
                        salary => salary.Emp_Id, // Join key for Salaries
                        emp => emp.Id, // Join key for Employees
                        (salary, emp) => new SalaryReportViewModel // Project into the ViewModel
                        {
                            Id = salary.Id,
                            Name = emp.Name,
                            Salary = emp.Salary,
                            Phone = emp.ContactNumber.ToString(),
                            Attendance_days = salary.Attendance_days,
                            Absent_days = salary.Absent_days,
                            Overtime_Hours = salary.Overtime_Hours,
                            Discount_Hours = salary.Discount_Hours,
                            Extra = salary.Extra,
                            Discount = salary.Discount,
                            Total = salary.Total
                        }
                    )
                    .FirstOrDefault(x => x.Id == id); // Filter by the provided Id

            return salaryData;
        }

        public Task<bool> IsSalaryExistInMonthAsync(int empId)
        {
            throw new NotImplementedException();
        }

        public Task InsertSalaryAsync(SalaryReport report)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSalaryAsync(SalaryReport report)
        {
            throw new NotImplementedException();
        }
    }
}
