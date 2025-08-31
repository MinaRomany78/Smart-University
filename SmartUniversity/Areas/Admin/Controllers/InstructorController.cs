using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Numerics;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public InstructorController(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var instructors = await _unitOfWork.Instructors.GetAsync(include: new Expression<Func<Instructor, object>>[]
            {
                e => e.ApplicationUser
            });

            if (instructors is null)
                return NotFound();

            var totalInstructorsInPage = 15;
            var totalPages = Math.Ceiling((double)instructors.Count() / totalInstructorsInPage);

            if (page > totalPages && totalPages != 0)
                return View(); ;

            instructors = instructors
                .Skip((page - 1) * (int)totalInstructorsInPage)
                .Take((int)totalInstructorsInPage);

            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(instructors);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new AdminInstructorVM
            {
                CoursesList = (await _unitOfWork.OptionalCourses.GetAsync()).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminInstructorVM vm)
        {
            if(!ModelState.IsValid)
            {
                vm.CoursesList = (await _unitOfWork.OptionalCourses.GetAsync()).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                }).ToList();
                return View(vm);
            }

            var user = new ApplicationUser
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                FullName = vm.FullName,
                Email = vm.Email,
                UserName = vm.UserName,
                Address = vm.Address,
                EmailConfirmed = vm.IsEmailConfirmed
            };

            var result = await _userManager.CreateAsync(user, "Pass123+");

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(vm);
            }

            if (!await _roleManager.RoleExistsAsync(SD.Instructor))
                await _roleManager.CreateAsync(new IdentityRole(SD.Instructor));

            await _userManager.AddToRoleAsync(user, SD.Instructor);

            var instructor = new Instructor
            {
                ApplicationUserId = user.Id
            };

            foreach (var courseId in vm.SelectedCourseIds)
            {
                var course = await _unitOfWork.OptionalCourses.GetOneAsync(e => e.Id == courseId);
                if (course is not null)
                    instructor.OptionalCourses.Add(course);
            }

            await _unitOfWork.Instructors.CreateAsync(instructor);
            await _unitOfWork.Instructors.CommitAsync();
            TempData["success-notification"] = "Instructor created successfully.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var instructor = _unitOfWork.Instructors.GetOneAsync(e => e.Id == id, include: new Expression<Func<Instructor, object>>[]
            {
                e => e.ApplicationUser,
                e => e.OptionalCourses
            }).Result;

            if (instructor is null)
                return NotFound();

            var vm = new AdminInstructorVM
            {
                Id = instructor.Id,
                FirstName = instructor.ApplicationUser.FirstName,
                LastName = instructor.ApplicationUser.LastName,
                FullName = instructor.ApplicationUser.FullName,
                Email = instructor.ApplicationUser.Email ?? "",
                UserName = instructor.ApplicationUser.UserName ?? "",
                Address = instructor.ApplicationUser.Address,
                IsEmailConfirmed = instructor.ApplicationUser.EmailConfirmed,
                SelectedCourseIds = instructor.OptionalCourses.Select(e => e.Id).ToList(),
                CoursesList = (await _unitOfWork.OptionalCourses.GetAsync()).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminInstructorVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CoursesList = (await _unitOfWork.OptionalCourses.GetAsync()).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                }).ToList();
                return View(vm);
            }

            var instructor = await _unitOfWork.Instructors.GetOneAsync(e => e.Id == vm.Id, include: new Expression<Func<Instructor, object>>[]
            {
                e => e.ApplicationUser,
                e => e.OptionalCourses
            });

            if (instructor is null)
                return NotFound();

            instructor.ApplicationUser.FirstName = vm.FirstName;
            instructor.ApplicationUser.LastName = vm.LastName;
            instructor.ApplicationUser.FullName = vm.FullName;
            instructor.ApplicationUser.Email = vm.Email;
            instructor.ApplicationUser.UserName = vm.UserName;
            instructor.ApplicationUser.Address = vm.Address;
            instructor.ApplicationUser.EmailConfirmed = vm.IsEmailConfirmed;

            instructor.OptionalCourses.Clear();
            if (vm.SelectedCourseIds != null && vm.SelectedCourseIds.Any())
            {
                foreach (var courseId in vm.SelectedCourseIds)
                {
                    var course = await _unitOfWork.OptionalCourses.GetOneAsync(e => e.Id == courseId);
                    if (course is not null)
                        instructor.OptionalCourses.Add(course);
                }
            }

            await _unitOfWork.Instructors.UpdateAsync(instructor);
            await _unitOfWork.Instructors.CommitAsync();

            TempData["success-notification"] = "Instructor updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _unitOfWork.Instructors.GetOneAsync(e => e.Id == id,
                include: new Expression<Func<Instructor, object>>[]
                {
                    e => e.OptionalCourses,
                    e => e.ApplicationUser
                });

            if (instructor is not null)
            {
                instructor.OptionalCourses.Clear();

                await _unitOfWork.Instructors.DeleteAsync(instructor);
                await _unitOfWork.Instructors.CommitAsync();

                TempData["success-notification"] = "Instructor Data Deleted Successfully";

                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
