using AspNetCoreGeneratedDocument;
using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class UniversityStudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public UniversityStudentController(
            IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var students = await _unitOfWork.Students.GetAsync(include: new Expression<Func<Student, object>>[]
            {
                e => e.ApplicationUser,
                e => e.Department,
                e => e.Term,
            });

            if (students is null)
                return NotFound();

            var totalStudentsInPage = 15;
            var totalPages = Math.Ceiling((double)students.Count() / totalStudentsInPage);
            if (page > totalPages && totalPages != 0)
                return View();

            students = students
                .Skip((page - 1) * (int)totalStudentsInPage)
                .Take((int)totalStudentsInPage);

            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string NationalId)
        {
            var vm = new AdminUniversityStudentVM
            {
                NationalId = NationalId,
                DepartMentList = (await _unitOfWork.Departments.GetAsync())
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToList(),

                TermList = new List<SelectListItem>() // هيتعبى بالـ Ajax
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminUniversityStudentVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.DepartMentList = (await _unitOfWork.Departments.GetAsync())
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToList();

                vm.TermList = new List<SelectListItem>();
                return View(vm);
            }

            var universityEmail = GenerateUniversityEmail(vm.FirstName, vm.LastName);
            var password = GeneratePassword();

            var user = new ApplicationUser
            {
                UserName = universityEmail.UserName,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                FullName = vm.FirstName + " " + vm.LastName,
                Email = universityEmail.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
                return View(vm);
            }

            var student = new Student
            {
                ApplicationUserId = user.Id,
                NationalID = vm.NationalId,
                IsUniversityStudent = true,
                DepartmentID = vm.DepartmentId,
                TermId = vm.TermId
            };

            await _userManager.AddToRoleAsync(user, SD.UniversityStudent);
            await _unitOfWork.Students.CreateAsync(student);

            var updateStudent = await _unitOfWork.Applications.GetOneAsync(e => e.NationalID == student.NationalID);

            updateStudent!.GenerateEmail = true;
            updateStudent!.GeneratedPassword = password;
            await _unitOfWork.Applications.UpdateAsync(updateStudent);
            await _unitOfWork.Applications.CommitAsync();
            await _unitOfWork.Students.CommitAsync();

            TempData["success-notification"] = "User Created successfully!";
            return RedirectToAction(nameof(Index), "UniversityStudent", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _unitOfWork.Students.GetOneAsync(
                s => s.Id == id,
                include: new Expression<Func<Student, object>>[] { e => e.ApplicationUser });

            if (student == null) return NotFound();

            var vm = new AdminUniversityStudentVM
            {
                Id = student.Id,
                NationalId = student.NationalID,
                FirstName = student.ApplicationUser.FirstName,
                LastName = student.ApplicationUser.LastName,
                DepartmentId = student.DepartmentID,
                TermId = student.TermId,
                DepartMentList = (await _unitOfWork.Departments.GetAsync())
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToList(),
                TermList = new List<SelectListItem>() // هيتعبى بالـ Ajax
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminUniversityStudentVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.DepartMentList = (await _unitOfWork.Departments.GetAsync())
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToList();
                vm.TermList = new List<SelectListItem>();
                return View(vm);
            }

            var student = await _unitOfWork.Students.GetOneAsync(
                s => s.Id == vm.Id,
                include: new Expression<Func<Student, object>>[] { e => e.ApplicationUser });

            if (student == null) return NotFound();

            // Update ApplicationUser
            var user = student.ApplicationUser;
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.FullName = vm.FirstName + " " + vm.LastName;
            await _userManager.UpdateAsync(user);

            // Update Student
            student.NationalID = vm.NationalId;
            student.TermId = vm.TermId;
            student.DepartmentID = vm.DepartmentId;

            await _unitOfWork.Students.UpdateAsync(student);
            await _unitOfWork.Students.CommitAsync();

            TempData["success-notification"] = "Student updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _unitOfWork.Students.GetOneAsync(e => e.Id == id,
                include: new Expression<Func<Student, object>>[] { e => e.ApplicationUser });

            if (student == null) return NotFound();

            var user = await _unitOfWork.ApplicationUsers.GetOneAsync(e => e.Id == student.ApplicationUserId);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);
            await _unitOfWork.Students.CommitAsync();

            TempData["success-notification"] = "User deleted successfully!";
            return RedirectToAction(nameof(Index), "UniversityStudent", new { area = "Admin" });
        }

        // ✅ API جديد للـ AJAX
        [HttpGet]
        public async Task<IActionResult> GetTermsByDepartment(int departmentId)
        {
            var terms = await _unitOfWork.Terms.GetAsync();

            // نفترض General = 1 , CS = 2 , IS = 3
            if (departmentId == 1) // General
                terms = terms.Where(t => t.Id <= 4);
            else // CS أو IS
                terms = terms.Where(t => t.Id > 4);

            var termList = terms.Select(t => new { t.Id, t.Name }).ToList();
            return Json(termList);
        }

        private (string UserName, string Email) GenerateUniversityEmail(string firstName, string lastName, string nationalId = "")
        {
            string first = firstName.Length >= 2 ? firstName.Substring(0, 2).ToLower() : firstName.ToLower();
            string last = lastName.ToLower();
            string unique = !string.IsNullOrEmpty(nationalId) ? nationalId.Substring(nationalId.Length - 3) : new Random().Next(100, 999).ToString();
            string userName = $"{first}{last}{unique}";
            string email = $"{userName}@smart-university.eg";
            return (userName, email);
        }

        private string GeneratePassword(int length = 10)
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%";

            var random = new Random();

            var password = new List<char>
            {
                upper[random.Next(upper.Length)],
                lower[random.Next(lower.Length)],
                digits[random.Next(digits.Length)],
                special[random.Next(special.Length)]
            };

            string allChars = upper + lower + digits + special;
            for (int i = password.Count; i < length; i++)
            {
                password.Add(allChars[random.Next(allChars.Length)]);
            }

            return new string(password.OrderBy(_ => random.Next()).ToArray());
        }
    }
}
