using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CheckoutController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public CheckoutController(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
          _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Success()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var student = await _unitOfWork.Students.GetOneAsync(s => s.ApplicationUserId == user.Id);
            if (student == null) return NotFound();

            var enrollments = await _unitOfWork.Enrollments.GetAsync(e => e.StudentID == student.Id);

            if (enrollments != null && enrollments.Any())
            {
                foreach (var enrollment in enrollments)
                {
                    enrollment.IsPaid = true;
                    await _unitOfWork.Enrollments.UpdateAsync(enrollment);
                }
            }

            ViewData["Message"] = "✅ Payment successful! Your courses have been paid.";
            return View();
        }

        public IActionResult Cancel()
        {
            ViewData["Message"] = "❌ Payment was cancelled. Please try again.";
            return View();
        }
    }
}
