using AssetsPro.Interfaces;
using AssetsPro.Models;
using AssetsPro.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AssetsPro.ViewModel;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using AssetsPro.Services;

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
        [HttpPost]
        public async Task<IActionResult> SaveRecords([FromBody] List<Attendance> records)
        {
            if (records == null || !records.Any() || !ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data" });

            try
            {
                var success = await service.SaveRecords(records);

                if (!success)
                    return Json(new { success = false, message = "Failed to save records" });

                await service.CalculateMonthlySalaryAsync(records);

                return Json(new { success = true, message = "Salaries calculated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //need separation of logic for Single Responsibility Principle (SOLD)[HttpPost]
        public async Task<IActionResult> CalculateSalaries()
        {
            var attendanceRecords = (await service.GetAll()).ToList() ;

            await service.CalculateMonthlySalaryAsync(attendanceRecords);

            return Ok("Salaries calculated successfully");
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
