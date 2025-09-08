using Microsoft.AspNetCore.Mvc;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           return RedirectToAction("Index", "Home", new { area = "Identity" });
        }
    }
}

