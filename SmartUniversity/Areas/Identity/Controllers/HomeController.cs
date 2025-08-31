using Microsoft.AspNetCore.Mvc;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identities is null)
            {
                return View();
            }

            if (User.IsInRole($"{SD.UniversityStudent}"))
            {

                return RedirectToAction("index", "UniversityStudent", new { area = "Customer" });
            }
            else if (User.IsInRole($"{SD.Doctor}"))
            {
                return RedirectToAction("index", "DoctorAndAssistant", new { area = "Customer" });

                return RedirectToAction("index", "UniversityStudent");
            }
            else if (User.IsInRole($"{SD.Doctor}"))
            {
                return RedirectToAction("index", "DoctorAndAssistant");
          }

            // لو مفيش Role مناسب
            return View();
        }





    }
}

