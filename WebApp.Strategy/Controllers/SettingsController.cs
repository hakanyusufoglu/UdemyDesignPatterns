using Microsoft.AspNetCore.Mvc;

namespace WebApp.Strategy.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
