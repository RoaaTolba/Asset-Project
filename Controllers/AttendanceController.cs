using AssetsPro.Interfaces;
using AssetsPro.Models;
using AssetsPro.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                return Json(new { success });
            }
            catch (Exception ex)
            {
                // Log the exception here as needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
