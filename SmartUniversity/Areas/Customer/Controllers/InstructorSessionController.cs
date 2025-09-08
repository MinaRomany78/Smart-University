using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = $"{SD.Instructor}")]
    public class InstructorSessionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public InstructorSessionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Create(int courseId)
        {
            var session = new CourseSessionVM { OptionalCourseId = courseId };
            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseSessionVM vm)
        {
            if (ModelState.IsValid)
            {
                var existingSessions = await _unitOfWork.CourseSessions.GetAsync(
                    e => e.OptionalCourseId == vm.OptionalCourseId);

                bool isFirstSession = !existingSessions.Any();

                var session = new CourseSession
                {
                    Title = vm.Title,
                    ReleaseDate = vm.ReleaseDate,
                    MaterialLink = vm.MaterialLink,
                    Order = vm.Order,
                    IsLocked = !isFirstSession,
                    OptionalCourseId = vm.OptionalCourseId
                };

                await _unitOfWork.CourseSessions.CreateAsync(session);
                await _unitOfWork.CourseSessions.CommitAsync();

                TempData["success-notification"] = "Session created successfully.";
                return RedirectToAction("Details", "InstructorCourse", new { area = "Customer", id = vm.OptionalCourseId });
            }
            return View(vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var session = await _unitOfWork.CourseSessions.GetOneAsync(e => e.Id == id);
            if (session == null)
                return NotFound();

            var vm = new CourseSessionVM
            {
                Id = session.Id,
                Title = session.Title,
                ReleaseDate = session.ReleaseDate,
                MaterialLink = session.MaterialLink,
                Order = session.Order,
                OptionalCourseId = session.OptionalCourseId
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseSessionVM vm)
        {
            if (ModelState.IsValid)
            {
                var session = await _unitOfWork.CourseSessions.GetOneAsync(e => e.Id == vm.Id);
                if (session == null)
                    return NotFound();

                session.Title = vm.Title;
                session.ReleaseDate = vm.ReleaseDate;
                session.MaterialLink = vm.MaterialLink;
                session.Order = vm.Order;
                session.OptionalCourseId = vm.OptionalCourseId;

                await _unitOfWork.CourseSessions.UpdateAsync(session);
                await _unitOfWork.CourseSessions.CommitAsync();

                TempData["success-notification"] = "Session updated successfully.";
                return RedirectToAction("Details", "InstructorCourse", new { area = "Customer", id = vm.OptionalCourseId });
            }
            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var session = await _unitOfWork.CourseSessions.GetOneAsync(
                e => e.Id == id,
                include: new Expression<Func<CourseSession, object>>[]
                {
                    e => e.Tasks
                });

            if (session == null)
                return NotFound();

            if (session.Tasks != null && session.Tasks.Any())
            {
                // خد نسخة من الليست عشان ما يحصلش تعديل أثناء الـ foreach
                var tasksToDelete = session.Tasks.ToList();

                foreach (var task in tasksToDelete)
                {
                    await _unitOfWork.SessionTasks.DeleteAsync(task);
                }
            }

            await _unitOfWork.CourseSessions.DeleteAsync(session);

            TempData["success-notification"] = "Session deleted successfully.";
            return RedirectToAction("Details", "InstructorCourse", new { area = "Customer", id = session.OptionalCourseId });
        }

        public async Task<IActionResult> ManageTasks(int sessionId)
        {
            var tasks = await _unitOfWork.SessionTasks.GetAsync(
                e => e.CourseSessionId == sessionId,
                include: new Expression<Func<SessionTask, object>>[]
                {
                    e => e.ApplicationUser
                });

            return View(tasks);
        }
    }
}
