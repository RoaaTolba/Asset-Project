using AssetsPro.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetsPro.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            return View();
        }
    }
}

