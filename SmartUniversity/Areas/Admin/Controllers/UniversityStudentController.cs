using AspNetCoreGeneratedDocument;
using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
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
        public UniversityStudentController(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
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
             e=>e.Department,
             e=>e.Term,
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

                TermList = (await _unitOfWork.Terms.GetAsync())
                    .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                    .ToList()
            };
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Create(AdminUniversityStudentVM vm)
        {
            if (!ModelState.IsValid)
            { vm.DepartMentList = (await _unitOfWork.Departments.GetAsync()).Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToList();

                vm.TermList = (await _unitOfWork.Terms.GetAsync()).Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name }).ToList();
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

            //await _emailSender.SendEmailAsync(
            //    updateStudent.Email,
            //    "Your Smart University Account",
            //    $"<p>Welcome to Smart University!</p>" +
            //    $"<p><b>Your university email:</b> {universityEmail}</p>" +
            //    $"<p><b>Your password:</b> {password}</p>" +
            //    $"<p>Please change your password after your first login.</p>"
            //    );

            TempData["success-notification"] = "User Created successfully!";
            return RedirectToAction(nameof(Index), "UniversityStudent", new { area = "Admin" }); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _unitOfWork.Students.GetOneAsync(
                s => s.Id == id,
                include: new Expression<Func<Student, object>>[] { e => e.ApplicationUser }
            );

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

                TermList = (await _unitOfWork.Terms.GetAsync())
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                 .ToList()
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

                vm.TermList = (await _unitOfWork.Terms.GetAsync())
                    .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                    .ToList();
                return View(vm);
            }

            var student = await _unitOfWork.Students.GetOneAsync(
                s => s.Id == vm.Id,
                include: new Expression<Func<Student, object>>[] {
                    e => e.ApplicationUser ,
                    e=>e.Term,
                    e=>e.Department,
                }
            );

            if (student == null) return NotFound();

            // Update ApplicationUser
            var user = student.ApplicationUser;
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.FullName = vm.FirstName + " " + vm.LastName;

            await _userManager.UpdateAsync(user);

            // Update Student
            student.NationalID = vm.NationalId;
            student.TermId= vm.TermId;
            student.DepartmentID= vm.DepartmentId;  

          await _unitOfWork.Students.UpdateAsync(student);
            await _unitOfWork.Students.CommitAsync();

            TempData["success-notification"] = "Student updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _unitOfWork.Students.GetOneAsync(e=>e.Id==id ,
                 include: new Expression<Func<Student, object>>[] { e => e.ApplicationUser });
            if (student == null) return NotFound();
            var user = await _unitOfWork.ApplicationUsers.GetOneAsync(e => e.Id == student.ApplicationUserId);
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);
            await _unitOfWork.Students.CommitAsync();
            await _unitOfWork.Students.CommitAsync();

            TempData["success-notification"] = "User deleted successfully!";

            return RedirectToAction(nameof(Index), "UniversityStudent", new { area = "Admin" });
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

            // لازم نضمن إن الباسورد فيه كل حاجة
            var password = new List<char>
            {
                upper[random.Next(upper.Length)],
                lower[random.Next(lower.Length)],
                digits[random.Next(digits.Length)],
                special[random.Next(special.Length)]
            };

            // نكمل لحد الطول المطلوب
            string allChars = upper + lower + digits + special;
            for (int i = password.Count; i < length; i++)
            {
                password.Add(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle
            return new string(password.OrderBy(_ => random.Next()).ToArray());
        }
    }
}
