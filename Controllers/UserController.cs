using Microsoft.AspNetCore.Mvc;

namespace AssetsPro.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
