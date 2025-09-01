using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OptionalCourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OptionalCourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var optionalCourses = await _unitOfWork.OptionalCourses.GetAsync(include: new Expression<Func<OptionalCourse, object>>[]
            {
                e => e.Instructor.ApplicationUser,
                e => e.PromoCodeEntity!
            });

            if (optionalCourses is null)
                return NotFound();

            var totalCoursesInPage = 15;
            var totalPages = Math.Ceiling((double)optionalCourses.Count() / totalCoursesInPage);

            if (page > totalPages && totalPages != 0)
                return View(); ;

            optionalCourses = optionalCourses
                .Skip((page - 1) * (int)totalCoursesInPage)
                .Take((int)totalCoursesInPage);

            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(optionalCourses);
        }

        public async Task<IActionResult> Create()
        {
            var instructors = await _unitOfWork.Instructors.GetAsync(include: new Expression<Func<Instructor, object>>[]
            {
                e => e.ApplicationUser
            });

            var vm = new OptionalCourseVM
            {
                Instructors = instructors.ToList(),
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OptionalCourseVM vm)
        {
            ModelState.Remove("MainImg");
            if (!ModelState.IsValid)
            {
                vm.Instructors = (await _unitOfWork.Instructors.GetAsync(include: new Expression<Func<Instructor, object>>[]
                {
                    e => e.ApplicationUser
                })).ToList();

                return View(vm);
            }

            var oCourse = new OptionalCourse
            {
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price,
                IsAvailableForUniversityStudents = vm.IsAvailableForUniversityStudents,
                InstructorId = vm.InstructorId
            };

            var promoCode = await _unitOfWork.PromoCodes.GetOneAsync(e => e.OptionalCourses.Any(e => e.Id == oCourse.Id));

            if (promoCode is not null)
            {
                oCourse.PromoCode = promoCode.Code;
            }

            if (vm.MainImg is not null && vm.MainImg.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.MainImg.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await vm.MainImg.CopyToAsync(stream);
                }

                oCourse.MainImg = fileName;
            }

            await _unitOfWork.OptionalCourses.CreateAsync(oCourse);
            await _unitOfWork.OptionalCourses.CommitAsync();

            TempData["success-notification"] = "Optional Course created successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var oCourse = await _unitOfWork.OptionalCourses.GetOneAsync(e => e.Id == id, include: new Expression<Func<OptionalCourse, object>>[]
            {
                e => e.Instructor,
                e => e.PromoCodeEntity!
            });

            if (oCourse is null)
                return NotFound();

            var instructors = await _unitOfWork.Instructors.GetAsync(include: new Expression<Func<Instructor, object>>[]
            {
                e => e.ApplicationUser
            });

            var vm = new OptionalCourseVM
            {
                Name = oCourse.Name,
                Description = oCourse.Description,
                Price = oCourse.Price,
                IsAvailableForUniversityStudents = oCourse.IsAvailableForUniversityStudents,
                InstructorId = oCourse.InstructorId,
                Instructors = instructors.ToList(),
            };

            ViewBag.mainImg = oCourse.MainImg;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OptionalCourseVM vm)
        {
            ModelState.Remove("MainImg");
            if (!ModelState.IsValid)
            {
                vm.Instructors = (await _unitOfWork.Instructors.GetAsync(include: new Expression<Func<Instructor, object>>[]
                {
                    e => e.ApplicationUser
                })).ToList();

                return View(vm);
            }

            var oCourse = await _unitOfWork.OptionalCourses.GetOneAsync(e => e.Id == id);

            if (oCourse is null)
                return NotFound();

            oCourse.Name = vm.Name;
            oCourse.Description = vm.Description;
            oCourse.Price = vm.Price;
            oCourse.IsAvailableForUniversityStudents = vm.IsAvailableForUniversityStudents;
            oCourse.InstructorId = vm.InstructorId;


            if (vm.MainImg is not null && vm.MainImg.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.MainImg.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await vm.MainImg.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(oCourse.MainImg))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", oCourse.MainImg);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                if (!string.IsNullOrEmpty(oCourse.MainImg))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", oCourse.MainImg);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                oCourse.MainImg = fileName;
            }

            await _unitOfWork.OptionalCourses.UpdateAsync(oCourse);
            await _unitOfWork.OptionalCourses.CommitAsync();

            TempData["success-notification"] = "Optional Course updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var oCourse = await _unitOfWork.OptionalCourses.GetOneAsync(e => e.Id == id);

            if (oCourse is null)
                return NotFound();

            if (!string.IsNullOrEmpty(oCourse.MainImg))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", oCourse.MainImg);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            await _unitOfWork.OptionalCourses.DeleteAsync(oCourse);
            await _unitOfWork.OptionalCourses.CommitAsync();

            TempData["success-notification"] = "Optional Course deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
