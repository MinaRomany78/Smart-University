using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(IUnitOfWork unitOfWork,IEmailSender emailSender,UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult NationalId() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NationalId(string nationalId)
        {
            var applicationData = await _unitOfWork.Applications
                .GetOneAsync(e => e.NationalID == nationalId);

            if (applicationData is null)
            {
                TempData["error-notification"] = "The National Id Not Found";
                return View();
            }

            var student = await _unitOfWork.Students
                .GetOneAsync(e => e.NationalID == nationalId);

            if (student == null)
            {
                TempData["error-notification"] = "Student data not found";
                return View();
            }

            var identityUser = await _userManager.FindByIdAsync(student.ApplicationUserId);

            // نجهز محتوى الإيميل بشكل HTML Card
            var emailBody = $@"
        <div style='max-width:500px;margin:auto;border:1px solid #ddd;border-radius:10px;overflow:hidden;font-family:Arial,sans-serif;'>
            <div style='background:#3a0ca3;color:#fff;padding:15px;text-align:center;font-size:20px;'>
                Student Profile
            </div>
            <div style='padding:20px;'>
                <p>Welcome to Smart University!</p>
                <p><strong>National ID:</strong> {student.NationalID}</p>
                <p><strong>Name:</strong> {identityUser!.FullName}</p>
                <p><strong>Email:</strong> {identityUser.Email}</p>
                <p><b>User Name:</b> {identityUser.UserName}</p>
                <p><b>password:</b> {applicationData.GeneratedPassword}</p>
                <p>Please change your password after your first login.</p>
            </div>
        </div>";

            await _emailSender.SendEmailAsync(applicationData.Email, "Your Student Profile", emailBody);

            TempData["success-notification"] = "Email sent successfully!";
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }


    }
}
