using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class AssistantController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssistantController(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var assistants = await _unitOfWork.Assistants.GetAsync(include: new Expression<Func<Assistant, object>>[]
            {
                e => e.ApplicationUser,
            });

            if (assistants is null)
                return NotFound();

            var totalAssistantsInPage = 15;
            var totalPages = Math.Ceiling((double)assistants.Count() / totalAssistantsInPage);

            if (page > totalPages && totalPages != 0)
                return View();

            assistants = assistants
                .Skip((page - 1) * (int)totalAssistantsInPage)
                .Take((int)totalAssistantsInPage);

            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(assistants);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new AdminAssistantVM
            {
                CoursesList = (await _unitOfWork.UniversityCourses.GetAsync())
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminAssistantVM vm)
        {
            var existingUserByName = await _userManager.FindByNameAsync(vm.UserName);
            if (existingUserByName != null)
                ModelState.AddModelError("UserName", "Username is already taken.");

            var existingUserByEmail = await _userManager.FindByEmailAsync(vm.Email);
            if (existingUserByEmail != null)
                ModelState.AddModelError("Email", "Email is already registered.");

            if (!ModelState.IsValid)
            {
                vm.CoursesList = (await _unitOfWork.UniversityCourses.GetAsync())
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
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
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, "Assistant123+");

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(vm);
            }

            if (!await _roleManager.RoleExistsAsync(SD.Assistant))
                await _roleManager.CreateAsync(new IdentityRole(SD.Assistant));

            await _userManager.AddToRoleAsync(user, SD.Assistant);

            var assistant = new Assistant
            {
                ApplicationUserId = user.Id
            };

            // 🔹 ربط الكورسات + الدكاترة
            foreach (var courseId in vm.SelectedCourseIds)
            {
                assistant.AssistantCourses.Add(new AssistantCourse
                {
                    CourseId = courseId
                });

                var doctorCourse = await _unitOfWork.UniversityCourses.GetOneAsync(
                    e => e.Id == courseId,
                    include: new Expression<Func<UniversityCourse, object>>[]
                    {
                        e => e.Doctor
                    });

                if (doctorCourse?.Doctor != null)
                {
                    if (!assistant.DoctorAssistants.Any(da => da.DoctorId == doctorCourse.Doctor.Id))
                    {
                        assistant.DoctorAssistants.Add(new DoctorAssistant
                        {
                            DoctorId = doctorCourse.Doctor.Id
                        });
                    }
                }
            }

            await _unitOfWork.Assistants.CreateAsync(assistant);
            await _unitOfWork.Assistants.CommitAsync();

            TempData["success-notification"] = "Assistant created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var assistant = await _unitOfWork.Assistants.GetOneAsync(
                e => e.Id == id,
                include: new Expression<Func<Assistant, object>>[]
                {
                    e => e.ApplicationUser,
                    e => e.AssistantCourses,
                    e => e.DoctorAssistants
                });

            if (assistant is null)
                return NotFound();

            var vm = new AdminAssistantVM
            {
                Id = assistant.Id,
                FirstName = assistant.ApplicationUser.FirstName,
                LastName = assistant.ApplicationUser.LastName,
                FullName = assistant.ApplicationUser.FullName,
                Email = assistant.ApplicationUser.Email ?? "",
                IsEmailConfirmed = assistant.ApplicationUser.EmailConfirmed,
                Address = assistant.ApplicationUser.Address,
                UserName = assistant.ApplicationUser.UserName ?? "",
                SelectedCourseIds = assistant.AssistantCourses.Select(ac => ac.CourseId).ToList()
            };

            var allCourses = await _unitOfWork.UniversityCourses.GetAsync();
            vm.CoursesList = allCourses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = vm.SelectedCourseIds.Contains(c.Id)
            }).ToList();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminAssistantVM vm)
        {
            var assistant = await _unitOfWork.Assistants.GetOneAsync(
                e => e.Id == vm.Id,
                include: new Expression<Func<Assistant, object>>[]
                {
                    e => e.ApplicationUser,
                    e => e.AssistantCourses,
                    e => e.DoctorAssistants
                });

            if (assistant is null)
                return NotFound();

            var existingUserByName = await _userManager.FindByNameAsync(vm.UserName);
            if (existingUserByName != null && existingUserByName.Id != assistant.ApplicationUserId)
                ModelState.AddModelError("UserName", "Username is already taken.");

            var existingUserByEmail = await _userManager.FindByEmailAsync(vm.Email);
            if (existingUserByEmail != null && existingUserByEmail.Id != assistant.ApplicationUserId)
                ModelState.AddModelError("Email", "Email is already registered.");

            if (!ModelState.IsValid)
            {
                var allCourses = await _unitOfWork.UniversityCourses.GetAsync();
                vm.CoursesList = allCourses.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = vm.SelectedCourseIds.Contains(c.Id)
                }).ToList();

                return View(vm);
            }

            // ✅ تحديث بيانات اليوزر
            assistant.ApplicationUser.FirstName = vm.FirstName;
            assistant.ApplicationUser.LastName = vm.LastName;
            assistant.ApplicationUser.FullName = vm.FullName;
            assistant.ApplicationUser.Email = vm.Email;
            assistant.ApplicationUser.Address = vm.Address;
            assistant.ApplicationUser.UserName = vm.UserName;
            assistant.ApplicationUser.EmailConfirmed = vm.IsEmailConfirmed;

            // ✅ Sync الكورسات
            var oldCourseIds = assistant.AssistantCourses.Select(ac => ac.CourseId).ToList();

            var toRemove = assistant.AssistantCourses.Where(ac => !vm.SelectedCourseIds.Contains(ac.CourseId)).ToList();
            foreach (var old in toRemove)
                await _unitOfWork.AssistantCourses.DeleteAsync(old);

            var toAdd = vm.SelectedCourseIds.Where(id => !oldCourseIds.Contains(id)).ToList();
            foreach (var courseId in toAdd)
            {
                assistant.AssistantCourses.Add(new AssistantCourse
                {
                    AssistantId = assistant.Id,
                    CourseId = courseId
                });

                var doctorCourse = await _unitOfWork.UniversityCourses.GetOneAsync(
                    e => e.Id == courseId,
                    include: new Expression<Func<UniversityCourse, object>>[]
                    {
                        e => e.Doctor
                    });

                if (doctorCourse?.Doctor != null)
                {
                    if (!assistant.DoctorAssistants.Any(da => da.DoctorId == doctorCourse.Doctor.Id))
                    {
                        assistant.DoctorAssistants.Add(new DoctorAssistant
                        {
                            DoctorId = doctorCourse.Doctor.Id,
                            AssistantId = assistant.Id
                        });
                    }
                }
            }

            await _unitOfWork.Assistants.UpdateAsync(assistant);
            await _unitOfWork.Assistants.CommitAsync();

            TempData["success-notification"] = "Assistant updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var assistant = await _unitOfWork.Assistants.GetOneAsync(e => e.Id == id,
                include: new Expression<Func<Assistant, object>>[]
                {
                    e => e.DoctorAssistants,
                    e => e.AssistantCourses,
                    e => e.ApplicationUser
                });

            if (assistant is not null)
            {
                assistant.DoctorAssistants.Clear();
                assistant.AssistantCourses.Clear();

                await _unitOfWork.Assistants.DeleteAsync(assistant);
                await _unitOfWork.Assistants.CommitAsync();

                TempData["success-notification"] = "Assistant Data Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
