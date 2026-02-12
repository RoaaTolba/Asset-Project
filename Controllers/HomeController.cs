using AssetsPro.Interfaces;
using AssetsPro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssetsPro.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        IEmpService empService;

        public HomeController(ILogger<HomeController> logger,IEmpService empService)
        {
            //_logger = logger;
            this.empService = empService;
        }

        public IActionResult Index()
        {
            return View(empService.GetAllEmp());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
