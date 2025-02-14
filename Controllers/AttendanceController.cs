using AssetsPro.Interfaces;
using AssetsPro.Models;
using AssetsPro.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AssetsPro.ViewModel;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AssetsPro.Controllers
{
    //[Authorize(Permessions.Attendance.Show)]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService service;

        public AttendanceController(IAttendanceService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            ViewData["Emps"] = service.GetEmpNames();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveRecords([FromBody] List<Attendance> records)
        {
            if (records == null || !records.Any() || !ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data" });
            }
            try
            {
                var success = await service.SaveRecords(records);
                CalculationFun(records);   
                return Json(new { success });
            }
            catch (Exception ex)
            {
                // Log the exception here as needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        private async void CalculationFun(List<Attendance> records)
        {
            int rec = 0;
            double discount_Hour = 0, overtime_Hour = 0, total = 0, totalExist=0, ex = 7, ds = 10; 
            List<EmpSalaryDataViewModel> GetEmpSalaryData = service.GetEmpSalaryData();
            foreach (var item in GetEmpSalaryData)
            {
                try { 
                        if (item.Emp_Id != records[rec].Emp_Id)
                        {
                            if (await service.isExistINmonth(item.Emp_Id))
                            {
                                var ExistSalaryReport = new SalaryReport
                                {
                                    //السالري لازم يقل منه علشان غاب يوم
                                    Emp_Id = item.Emp_Id,
                                    Attendance_days = 0,
                                    Absent_days = 1,
                                    Overtime_Hours = 0,
                                    Discount_Hours = 0,
                                    Extra = 0,
                                    Discount = 0,
                                    Total = 0
                                };
                                service.updateSalary(ExistSalaryReport);
                            }
                            else
                            {
                                var newSalaryReport = new SalaryReport
                                {
                                    //السالري لازم يقل منه علشان غاب يوم
                                    Emp_Id = item.Emp_Id,
                                    Attendance_days = 0,
                                    Absent_days = 1,
                                    Overtime_Hours = 0,
                                    Discount_Hours = 0,
                                    Extra = 0,
                                    Discount = 0,
                                    Total = item.Salary - 100
                                };
                                service.insertSalary(newSalaryReport);
                            }
                    }
                        else
                        {
                        //start
                        TimeSpan recStartTime = TimeSpan.ParseExact(records[rec].StartTime, "hh\\:mm", CultureInfo.InvariantCulture);
                        TimeSpan EmpStartTime = TimeSpan.ParseExact(item.StartTime, "hh\\:mm", CultureInfo.InvariantCulture);
                        TimeSpan differenceStartTime = recStartTime - EmpStartTime;
                        double start = differenceStartTime.TotalHours;
                        //End
                        TimeSpan recEndTime = TimeSpan.ParseExact(records[rec].EndTime, "hh\\:mm", CultureInfo.InvariantCulture);
                        TimeSpan EmpEndTime = TimeSpan.ParseExact(item.EndTime, "hh\\:mm", CultureInfo.InvariantCulture);
                        TimeSpan differenceEndTime = recEndTime - EmpEndTime;
                        double End = differenceEndTime.TotalHours;

                            ////start
                            //TimeSpan differenceStartTime = records[rec].StartTime - item.StartTime;
                            //    double start = differenceStartTime.TotalHours;
                            //    //End
                            //    TimeSpan differenceEndTime = records[rec].EndTime - item.EndTime;
                            //    double End = differenceEndTime.TotalHours;

                            if (start > 0)
                                discount_Hour += start;
                            else if (start < 0)
                                overtime_Hour += -start;
                            if (End < 0)
                                discount_Hour += -End;
                            else if (End > 0)
                                overtime_Hour += End;
                            if (await service.isExistINmonth(item.Emp_Id))
                            {
                                totalExist = (overtime_Hour * ex) - (discount_Hour * ds);
                                var ExistSalaryReport = new SalaryReport
                                {
                                    Emp_Id = item.Emp_Id,
                                    Attendance_days = 1,
                                    Absent_days = 0,
                                    Overtime_Hours = overtime_Hour,
                                    Discount_Hours = discount_Hour,
                                    Extra = (int)(overtime_Hour * ex),
                                    Discount = (int)(discount_Hour * ds),
                                    Total = (int)totalExist
                                };
                                service.updateSalary(ExistSalaryReport);
                            }
                            else
                            {
                                total = (double)item.Salary + (overtime_Hour * ex) - (discount_Hour * ds);
                                var newSalaryReport = new SalaryReport
                                {
                                    Emp_Id = item.Emp_Id,
                                    Attendance_days = 1,
                                    Absent_days = 0,
                                    Overtime_Hours = overtime_Hour,
                                    Discount_Hours = discount_Hour,
                                    Extra = (int)(overtime_Hour * ex),
                                    Discount = (int)(discount_Hour * ds),
                                    Total = (int)total
                                };
                                service.insertSalary(newSalaryReport);
                            }
                            rec++;
                            overtime_Hour = discount_Hour = 0;
                        }
                }
                catch(ArgumentOutOfRangeException) {
                    if (await service.isExistINmonth(item.Emp_Id)){
                        var ExistSalaryReport = new SalaryReport
                        {
                            //السالري لازم يقل منه علشان غاب يوم
                            Emp_Id = item.Emp_Id,
                            Attendance_days = 0,
                            Absent_days = 1,
                            Overtime_Hours = 0,
                            Discount_Hours = 0,
                            Extra = 0,
                            Discount = 0,
                            Total = 0
                        };
                        service.updateSalary(ExistSalaryReport);
                    }
                    else{
                        var newSalaryReport = new SalaryReport
                        {
                            //السالري لازم يقل منه علشان غاب يوم
                            Emp_Id = item.Emp_Id,
                            Attendance_days = 0,
                            Absent_days = 1,
                            Overtime_Hours = 0,
                            Discount_Hours = 0,
                            Extra = 0,
                            Discount = 0,
                            Total = item.Salary -100
                        };
                        service.insertSalary(newSalaryReport);
                    }
                }
                
            }
        }
        public IActionResult ShowSalaryReport() 
        {
            return View(service.salaryReportData());
        }
        public IActionResult Invoice(int id)
        {
            return View(service.findSalaryById(id));
        }
    }
}
