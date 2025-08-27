
using Microsoft.AspNetCore.Mvc;

namespace WebPL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => RedirectToAction("Index", "Drones");
    }
}
