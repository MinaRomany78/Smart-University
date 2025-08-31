using AspNetCoreGeneratedDocument;
using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Admin.Controllers
{ [Area("Admin")]
    public class UniversityStudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UniversityStudentController(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
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
            {
                vm.DepartMentList = (await _unitOfWork.Departments.GetAsync())
                 .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                 .ToList();

                vm.TermList = (await _unitOfWork.Terms.GetAsync())
                    .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                    .ToList();
                return View(vm);
            }

            var user = new ApplicationUser
            {
                UserName = vm.UserName,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                FullName=vm.FirstName+vm.LastName,
                Email = vm.Email,
                EmailConfirmed=vm.IsEmailConfirmed,

            };

            var result = await _userManager.CreateAsync(user, "Pass123+");
            
            if (!result.Succeeded)
            {
               
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(vm);
            }

            var student = new Student
            {
                ApplicationUserId = user.Id,
                NationalID = vm.NationalId,
                IsUniversityStudent=true,
                DepartmentID = vm.DepartmentId,
                TermId = vm.TermId

            };
            await _userManager.AddToRoleAsync(user, SD.UniversityStudent);

            await _unitOfWork.Students.CreateAsync(student);
           var updateStudent =await _unitOfWork.Applications.GetOneAsync(e=>e.NationalID==student.NationalID);
            
            updateStudent!.GenerateEmail = true;

            await _unitOfWork.Applications.UpdateAsync(updateStudent);
            await _unitOfWork.Students.CommitAsync();
            TempData["success-notification"] = "User Created successfully!";


            return RedirectToAction(nameof(Index), "UniversityStudent", new {area="Admin"});
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
                UserName = student!.ApplicationUser.UserName??"",
                FirstName = student.ApplicationUser.FirstName,
                LastName = student.ApplicationUser.LastName,
                Email = student!.ApplicationUser.Email??"",
                IsEmailConfirmed = student.ApplicationUser.EmailConfirmed,
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
            user.UserName = vm.UserName;
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.FullName = vm.FirstName + " " + vm.LastName;
            user.Email = vm.Email;
            user.EmailConfirmed = vm.IsEmailConfirmed;

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

    }
}
