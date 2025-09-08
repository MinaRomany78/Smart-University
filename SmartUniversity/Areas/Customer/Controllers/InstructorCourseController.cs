using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = $"{SD.Instructor}")]
    public class InstructorCourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        public InstructorCourseController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();

            var instructor = await _unitOfWork.Instructors.GetOneAsync(e => e.ApplicationUserId == user.Id,
                include: new Expression<System.Func<Instructor, object>>[]
                {
                    e => e.ApplicationUser
                });

            if (instructor is null)
                return NotFound();

            var courses = await _unitOfWork.OptionalCourses.GetAsync(e => e.InstructorId == instructor.Id);

            return View(courses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await _unitOfWork.OptionalCourses.GetOneAsync(e => e.Id == id,
                include: new Expression<Func<OptionalCourse, object>>[]
                {
                    e => e.Sessions
                });

            if (course is null)
                return NotFound();

            return View(course);
        }

        [HttpGet]
        public IActionResult ApplyNewCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyNewCourse(NewCourseApplicationVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var subject = $"New Course Application : {model.CourseName}";
            var body = $@"
                Instructor {user.FullName} ({user.Email}) is applying for a new course.<br/><br/>
                <b>Course Name:</b> {model.CourseName}<br/>
                <b>Experience:</b> {model.Experience}<br/>
            ";

            var adminEmail = "ahmedkmuhamedd@gmail.com";

            await _emailSender.SendEmailAsync(adminEmail, subject, body);

            TempData["success-notification"] = "Your request has been sent to the admin. They will contact you via email.";
            return RedirectToAction(nameof(Index));
        }
    }
}
