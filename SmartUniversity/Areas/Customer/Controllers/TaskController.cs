using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class TaskController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Tasks(int courseId, string color)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["error-notification"] = "User not found!";
                return NotFound();
            }

            var doctor = await _unitOfWork.Doctors.GetOneAsync(d => d.ApplicationUserId == user.Id);
            var assistant = await _unitOfWork.Assistants.GetOneAsync(a => a.ApplicationUserId == user.Id);
            var student = await _unitOfWork.Students.GetOneAsync(s => s.ApplicationUserId == user.Id);

            IEnumerable<SubjectTask> tasks = new List<SubjectTask>();

            if (doctor != null)
            {
                tasks = await _unitOfWork.SubjectTasks.GetAsync(
                    t => t.DoctorId == doctor.Id && t.UniversityCourseID == courseId,
                    include: new Expression<Func<SubjectTask, object>>[] { t => t.UniversityCourse, t => t.Feedbacks }
                );
            }
            else if (assistant != null)
            {
                tasks = await _unitOfWork.SubjectTasks.GetAsync(
                    t => t.AssistantId == assistant.Id && t.UniversityCourseID == courseId,
                    include: new Expression<Func<SubjectTask, object>>[] { t => t.UniversityCourse, t => t.Feedbacks }
                );
            }
            else if (student != null)
            {
                tasks = await _unitOfWork.SubjectTasks.GetAsync(
                    t => t.UniversityCourseID == courseId &&
                         t.UniversityCourse.Enrollments.Any(e => e.StudentID == student.Id),
                    include: new Expression<Func<SubjectTask, object>>[]
                    {
                        t => t.UniversityCourse,
                        t => t.Feedbacks.Where(f => f.StudentID == student.Id),
                        t => t.TaskSubmissions
                    }
                );
                ViewBag.StudentId = student.Id;
            }

            ViewData["ActiveTab"] = "Tasks";
            ViewData["CourseId"] = courseId;
            ViewData["Color"] = color?.StartsWith("#") == true ? color : "#" + (color ?? "0d6efd");

            return View(tasks);
        }

        [HttpGet]
        public IActionResult CreateTask(int courseId, string color)
        {
            var vm = new TaskVM
            {
                UniversityCourseID = courseId,
                Deadline = DateTime.Now.AddDays(7).AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerMinute))
            };

            ViewData["Color"] = color;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskVM vm, string color)
        {
            if (!ModelState.IsValid)
            {
                TempData["error-notification"] = "Invalid data!";
                return View(vm);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var doctor = await _unitOfWork.Doctors.GetOneAsync(d => d.ApplicationUserId == user.Id);
            var assistant = await _unitOfWork.Assistants.GetOneAsync(a => a.ApplicationUserId == user.Id);

            var task = new SubjectTask
            {
                UniversityCourseID = vm.UniversityCourseID,
                Title = vm.Title,
                Description = vm.Description,
                Deadline = vm.Deadline,
                DoctorId = doctor?.Id,
                AssistantId = assistant?.Id
            };

            await _unitOfWork.SubjectTasks.CreateAsync(task);
            TempData["success-notification"] = "Task added successfully ✅";

            return RedirectToAction(nameof(Tasks), new { courseId = vm.UniversityCourseID, color });
        }

        [HttpGet]
        public async Task<IActionResult> EditTask(int id, string color)
        {
            var task = await _unitOfWork.SubjectTasks.GetOneAsync(t => t.Id == id);
            if (task == null)
            {
                TempData["error-notification"] = "Task not found!";
                return NotFound();
            }

            var vm = new TaskVM
            {
                Id = task.Id,
                UniversityCourseID = task.UniversityCourseID,
                Title = task.Title,
                Description = task.Description,
                Deadline = task.Deadline
            };

            ViewData["Color"] = color;
            return View(vm);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> EditTask(TaskVM vm, string color)
        {
            if (!ModelState.IsValid)
            {
                TempData["error-notification"] = "Invalid data!";
                return View(vm);
            }

            var task = await _unitOfWork.SubjectTasks.GetOneAsync(
                t => t.Id == vm.Id,
                include: new Expression<Func<SubjectTask, object>>[]
                {
            t => t.TaskSubmissions
                }
            );

            if (task == null)
            {
                TempData["error-notification"] = "Task not found!";
                return NotFound();
            }

            // 🟢 تحديث بيانات التاسك
            task.Title = vm.Title;
            task.Description = vm.Description;
            task.Deadline = vm.Deadline;
            await _unitOfWork.SubjectTasks.UpdateAsync(task);

            // 🟢 تحديث Status بتاع كل Submission
            if (task.TaskSubmissions != null && task.TaskSubmissions.Any())
            {
                foreach (var sub in task.TaskSubmissions)
                {
                    if (sub.SubmissionDate != null)
                    {
                        sub.Status = (sub.SubmissionDate <= task.Deadline)
                            ? SubmissionStatus.OnTime
                            : SubmissionStatus.Late;

                        await _unitOfWork.TaskSubmissions.UpdateAsync(sub);
                    }
                }
            }

            TempData["success-notification"] = "Task updated successfully ✅";
            return RedirectToAction(nameof(Tasks), new { courseId = task.UniversityCourseID, color });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTask(int id, int courseId, string color)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var task = await _unitOfWork.SubjectTasks.GetOneAsync(
                t => t.Id == id,
                include: new Expression<Func<SubjectTask, object>>[]
                {
            t => t.Feedbacks,
            t => t.TaskSubmissions
                }
            );

            if (task == null)
            {
                TempData["error-notification"] = "Task not found!";
                return NotFound();
            }

            // 🟢 امسح كل Feedbacks
            if (task.Feedbacks != null && task.Feedbacks.Any())
            {
                foreach (var fb in task.Feedbacks.ToList())
                {
                    await _unitOfWork.Feedbacks.DeleteAsync(fb);
                }
            }

            // 🟢 امسح كل Submissions
            if (task.TaskSubmissions != null && task.TaskSubmissions.Any())
            {
                foreach (var sub in task.TaskSubmissions.ToList())
                {
                    await _unitOfWork.TaskSubmissions.DeleteAsync(sub);
                }
            }

            // 🟢 امسح التاسك نفسه
            bool status = await _unitOfWork.SubjectTasks.DeleteAsync(task);

            if (status)
            {
                TempData["success-notification"] = "Task and all related data deleted successfully 🗑️";
                return RedirectToAction(nameof(Tasks), new { courseId, color });
            }

            TempData["error-notification"] = "Failed to delete task!";
            return View(task);
        }


        [HttpGet]
        public IActionResult SubmitTask(int taskId, int courseId, string color)
        {
            var vm = new TaskSubmissionCreateVM
            {
                TaskId = taskId,
                CourseId = courseId,
                Color = color
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTask(TaskSubmissionCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["error-notification"] = "Invalid submission data!";
                return View(vm);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var student = await _unitOfWork.Students.GetOneAsync(s => s.ApplicationUserId == user.Id);
            if (student == null) return Unauthorized();

            var task = await _unitOfWork.SubjectTasks.GetOneAsync(t => t.Id == vm.TaskId);
            if (task == null)
            {
                TempData["error-notification"] = "Task not found!";
                return NotFound();
            }

            var submission = new TaskSubmission
            {
                TaskID = vm.TaskId,
                StudentID = student.Id,
                SubmissionDate = DateTime.Now,
                SubmissionLink = vm.SubmissionLink,
                Status = (DateTime.Now <= task.Deadline) ? SubmissionStatus.OnTime : SubmissionStatus.Late
            };

            await _unitOfWork.TaskSubmissions.CreateAsync(submission);
            TempData["success-notification"] = "Submission sent successfully 📤";

            return RedirectToAction(nameof(Tasks), new { courseId = vm.CourseId, color = vm.Color });
        }

        [HttpPost]
        public async Task<IActionResult> CancelSubmission(int submissionId, int courseId, string color)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var student = await _unitOfWork.Students.GetOneAsync(s => s.ApplicationUserId == user.Id);
            if (student == null) return Unauthorized();

            var submission = await _unitOfWork.TaskSubmissions.GetOneAsync(
                s => s.Id == submissionId && s.StudentID == student.Id
            );
            if (submission == null)
            {
                TempData["error-notification"] = "Submission not found!";
                return NotFound();
            }

            // 🟢 امسح الفيدباك الخاصة بالطالب في نفس التاسك
            var feedbacks = await _unitOfWork.Feedbacks.GetAsync(
                f => f.TaskID == submission.TaskID && f.StudentID == student.Id
            );

            foreach (var fb in feedbacks.ToList())
            {
                await _unitOfWork.Feedbacks.DeleteAsync(fb);
            }

            // 🟢 امسح الـ Submission نفسه
            await _unitOfWork.TaskSubmissions.DeleteAsync(submission);

            TempData["success-notification"] = "Submission and related feedback deleted 🗑️";

            return RedirectToAction(nameof(Tasks), new { courseId, color });
        }


        public async Task<IActionResult> Submissions(int taskId, string color)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var doctor = await _unitOfWork.Doctors.GetOneAsync(d => d.ApplicationUserId == user.Id);
            var assistant = await _unitOfWork.Assistants.GetOneAsync(a => a.ApplicationUserId == user.Id);

            if (doctor == null && assistant == null)
            {
                TempData["error-notification"] = "You are not authorized to view submissions!";
                return NotFound();
            }

            var task = await _unitOfWork.SubjectTasks.GetTaskWithSubmissionsAsync(taskId);
            if (task == null)
            {
                TempData["error-notification"] = "Task not found!";
                return NotFound();
            }

            var submissionsVM = task.TaskSubmissions.Select(s => new TaskSubmissionVM
            {
                SubmissionId = s.Id,
                TaskId = task.Id,
                TaskTitle = task.Title,
                StudentName = s.Student?.ApplicationUser?.FullName ?? "Unknown Student",
                StudentId = s.StudentID,
                SubmissionDate = s.SubmissionDate,
                Grade = s.Grade,
                Status = s.Status,
                CourseId = task.UniversityCourseID,
                SubmissionLink = s.SubmissionLink
            }).ToList();

            ViewData["Color"] = color;
            ViewData["CourseId"] = task.UniversityCourseID;

            return View(submissionsVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGrade(int submissionId, int taskId, int courseId, string color, double grade)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var doctor = await _unitOfWork.Doctors.GetOneAsync(d => d.ApplicationUserId == user.Id);
            var assistant = await _unitOfWork.Assistants.GetOneAsync(a => a.ApplicationUserId == user.Id);

            if (doctor == null && assistant == null)
            {
                TempData["error-notification"] = "You are not authorized to update grades!";
                return Forbid();
            }

            var submission = await _unitOfWork.TaskSubmissions.GetOneAsync(s => s.Id == submissionId);
            if (submission == null)
            {
                TempData["error-notification"] = "Submission not found!";
                return NotFound();
            }

            submission.Grade = (decimal)grade;
            await _unitOfWork.TaskSubmissions.UpdateAsync(submission);
            TempData["success-notification"] = "Grade updated successfully 🎯";

            return RedirectToAction("Submissions", new { taskId, color });
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback(int taskId, int studentId, int courseId, string color, int rating, string comment)
        {
            var task = await _unitOfWork.SubjectTasks.GetOneAsync(t => t.Id == taskId);
            var student = await _unitOfWork.Students.GetOneAsync(s => s.Id == studentId);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["error-notification"] = "User not found!";
                return NotFound();
            }
            var assistant = await _unitOfWork.Assistants.GetOneAsync(a => a.ApplicationUserId == user.Id);

            if (task == null || student == null || assistant == null)
            {
                TempData["error-notification"] = "Invalid data, feedback not saved!";
                return NotFound();
            }

            var feedback = new Feedback
            {
                TaskID = taskId,
                StudentID = studentId,
                AssistantID = assistant.Id,
                Rating = rating,
                Comment = comment
            };

            await _unitOfWork.Feedbacks.CreateAsync(feedback);
            TempData["success-notification"] = "Feedback added successfully 📝";

            return RedirectToAction("Submissions", new { taskId, courseId, color });
        }
    }
}
