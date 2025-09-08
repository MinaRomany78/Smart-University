using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       
        public async Task<IActionResult> Index(int page = 1)
        {
            var courses = await _unitOfWork.UniversityCourses.GetAsync(
                include: new Expression<Func<UniversityCourse, object>>[]
                {
                    e => e.Department,
                    e => e.Term
                });

            if (courses == null)
                return NotFound();

            int totalUniversityCoursesInPage = 15;
            var totalPages = Math.Ceiling((double)courses.Count() / totalUniversityCoursesInPage);

            if (page > totalPages && totalPages != 0)
                return View();

            courses = courses
                .Skip((page - 1) * totalUniversityCoursesInPage)
                .Take(totalUniversityCoursesInPage);

            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(courses);
        }

        // GET: Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new AdminCourseVM
            {
                DepartMentList = (await _unitOfWork.Departments.GetAsync())
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToList(),

                TermList = (await _unitOfWork.Terms.GetAsync())
                    .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
     
        public async Task<IActionResult> Create(AdminCourseVM courseVM)
        {
            if (!ModelState.IsValid)
            {
                courseVM.DepartMentList = (await _unitOfWork.Departments.GetAsync())
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToList();

                courseVM.TermList = (await _unitOfWork.Terms.GetAsync())
                    .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                    .ToList();

                return View(courseVM);
            }

            var newCourse = new UniversityCourse
            {
                Name = courseVM.Name,
                CreditHours = courseVM.CreditHours,
                DepartmentID = courseVM.DepartmentId,
                TermId = courseVM.TermId
            };

            await _unitOfWork.UniversityCourses.CreateAsync(newCourse);
            await _unitOfWork.UniversityCourses.CommitAsync();

            TempData["success-notification"] = "Course Created successfully!";
            return RedirectToAction(nameof(Index), "Course", new { area = "Admin" });
        }

        // GET: Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _unitOfWork.UniversityCourses.GetOneAsync(e => e.Id == id);
            if (course == null)
                return NotFound();

            var model = new AdminCourseVM
            {
                Id = course.Id,
                Name = course.Name,
                CreditHours = course.CreditHours,
                DepartmentId = course.DepartmentID,
                TermId = course.TermId,
                DepartMentList = (await _unitOfWork.Departments.GetAsync())
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToList(),
                TermList = (await _unitOfWork.Terms.GetAsync())
                    .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
       
        public async Task<IActionResult> Edit(AdminCourseVM courseVM)
        {
            if (!ModelState.IsValid)
            {
                courseVM.DepartMentList = (await _unitOfWork.Departments.GetAsync())
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToList();

                courseVM.TermList = (await _unitOfWork.Terms.GetAsync())
                    .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                    .ToList();

                return View(courseVM);
            }

            var courseUpdate = await _unitOfWork.UniversityCourses.GetOneAsync(e => e.Id == courseVM.Id);
            if (courseUpdate == null)
                return NotFound();

            courseUpdate.Name = courseVM.Name;
            courseUpdate.CreditHours = courseVM.CreditHours;
            courseUpdate.DepartmentID = courseVM.DepartmentId;
            courseUpdate.TermId = courseVM.TermId;

            await _unitOfWork.UniversityCourses.UpdateAsync(courseUpdate);
            await _unitOfWork.UniversityCourses.CommitAsync();

            TempData["success-notification"] = "Course Updated successfully!";
            return RedirectToAction(nameof(Index), "Course", new { area = "Admin" });
        }

    
        public async Task<IActionResult> Delete(int id)
        {
            var courseRemove = await _unitOfWork.UniversityCourses.GetOneAsync(e => e.Id == id);
            if (courseRemove == null)
                return NotFound();

            await _unitOfWork.UniversityCourses.DeleteAsync(courseRemove);
            await _unitOfWork.UniversityCourses.CommitAsync();

            TempData["success-notification"] = "Course Deleted successfully!";
            return RedirectToAction(nameof(Index), "Course", new { area = "Admin" });
        }
    }
}
