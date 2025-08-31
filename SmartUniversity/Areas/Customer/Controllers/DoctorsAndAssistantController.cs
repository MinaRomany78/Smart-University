using Microsoft.AspNetCore.Mvc;

namespace SmartUniversity.Areas.Customer.Controllers
{
    public class DoctorsAndAssistantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
