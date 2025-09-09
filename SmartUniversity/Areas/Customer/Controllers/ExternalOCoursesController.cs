using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = $"{SD.UniversityStudent},{SD.ExternalStudent},{SD.Instructor}")]
    public class ExternalOCoursesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public ExternalOCoursesController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();

            var courses = await _unitOfWork.UserOptionalCourses.GetAsync(e => e.ApplicationUserId == user.Id,
                include : new Expression<Func<UserOptionalCourse, object>>[]
                {
                    e => e.OptionalCourse,
                    e => e.OptionalCourse.Instructor,
                    e => e.OptionalCourse.Instructor.ApplicationUser
                });

            if (courses is null)
                return NotFound();

            return View(courses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();
            var course = await _unitOfWork.UserOptionalCourses.GetOneAsync(e => e.ApplicationUserId == user.Id && e.OptionalCourseId == id,
                include: new Expression<Func<UserOptionalCourse, object>>[]
                {
                    e => e.OptionalCourse,
                    e => e.OptionalCourse.Instructor,
                    e => e.OptionalCourse.Instructor.ApplicationUser,
                    e => e.OptionalCourse.Sessions
                });

            if (course is null)
                return NotFound();

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTask(int sessionId, string submissionLink)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var session = await _unitOfWork.CourseSessions.GetOneAsync(e => e.Id == sessionId);
            if (session == null)
                return NotFound();

            var existingTask = await _unitOfWork.SessionTasks
                .GetOneAsync(e => e.CourseSessionId == sessionId && e.ApplicationUserId == user.Id);

            if (existingTask != null)
            {
                existingTask.SubmissionLink = submissionLink;
                existingTask.SubmittedAt = DateTime.Now;
                existingTask.IsCompleted = true;

                await _unitOfWork.SessionTasks.UpdateAsync(existingTask);
                TempData["success-notification"] = "Task updated successfully!";
            }
            else
            {
                var task = new SessionTask
                {
                    Title = $"Submission for {session.Title}",
                    CourseSessionId = sessionId,
                    ApplicationUserId = user.Id,
                    SubmissionLink = submissionLink,
                    SubmittedAt = DateTime.Now,
                    IsCompleted = true
                };

                await _unitOfWork.SessionTasks.CreateAsync(task);

                var nextSession = await _unitOfWork.CourseSessions.GetOneAsync(e => e.OptionalCourseId == session.OptionalCourseId && e.Order == session.Order + 1);
                if (nextSession != null)
                {
                    nextSession.IsLocked = false;
                    await _unitOfWork.CourseSessions.UpdateAsync(nextSession);
                }

                TempData["success-notification"] = "Task submitted successfully!";
            }

            await _unitOfWork.SessionTasks.CommitAsync();
            return RedirectToAction("Details", new { id = session.OptionalCourseId });
        }
    }
}
