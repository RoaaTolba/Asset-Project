using AssetsPro.Interfaces;
using AssetsPro.Models;
using AssetsPro.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;

namespace AssetsPro.Controllers
{
    public class EmployeeController : Controller
    {
        IEmpService empService;
        public EmployeeController( IEmpService empService)
        {
            this.empService = empService;
        }
        [Authorize(Permessions.Employee.Show)]
        public IActionResult Index()
        {
            return View(empService.GetAllEmp());
        }
        [Authorize(Permessions.Employee.Add)]
        public IActionResult AddEmp()
        {  
            ViewData["Gender"] = empService.GetAllGender();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SaveAddEmp(Employee newemp)
        {
            if (ModelState.IsValid)
            {
                var success = empService.AddEmp(newemp);
                if (success) return RedirectToAction("Index"); 
            }
            ViewData["Gender"] = empService.GetAllGender();   
            return View("AddEmp",newemp);
        }
        //Edit and Delete still didn't it
        [Authorize(Permessions.Employee.Edit)]
        public IActionResult Edit(int id)
        {
            ViewData["Gender"] = empService.GetAllGender();
            
            return View(empService.GetbyId(id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SaveEdit(int id, Employee newEmp)
        {
            if (ModelState.IsValid)
            {
                var succes = empService.SaveEdit(id, newEmp);
                if(succes) return RedirectToAction("Index");
            }
            ViewData["Gender"] = empService.GetAllGender();

            return View("Edit",newEmp);
        }
        [HttpPost]
        [Authorize(Permessions.Employee.Delete)]
        public IActionResult Delete(int id)
        {
            var succes = empService.deleteEmp(id);

            if (succes)
            {
                TempData["Message"] = "Employee deleted successfully.";
                return RedirectToAction("Index"); 
            }
            TempData["AlertMessage"] = "Employee not found or could not be deleted.";
            return RedirectToAction("Index");
             
        }
        public IActionResult Details(int id)
        {
            return View(empService.GetbyId(id));
        }
    }
}
