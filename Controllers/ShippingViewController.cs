using Microsoft.AspNetCore.Mvc;

namespace OrderManagementSystem.Controllers
{
    public class ShippingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tracking()
        {
            return View();
        }
    }
}
