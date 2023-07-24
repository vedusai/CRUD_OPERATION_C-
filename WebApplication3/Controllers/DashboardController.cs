using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
