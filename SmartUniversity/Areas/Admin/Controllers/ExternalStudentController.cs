using DataAccess.Repositories;
using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class ExternalStudentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ExternalStudentController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(SD.ExternalStudent);
            if (usersInRole == null )
                return NotFound();

            int totalInPage = 15;
            var totalPages = Math.Ceiling((double)usersInRole.Count / totalInPage);

            if (page > totalPages && totalPages != 0)
                return View();

            var pagedUsers = usersInRole
                .AsQueryable()
                .Skip((page - 1) * totalInPage)
                .Take(totalInPage)
                .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(pagedUsers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminExternalStudentVM externalStudentVM)
        {
            ModelState.Remove(nameof(externalStudentVM.Id));

            if (!ModelState.IsValid)
                return View(externalStudentVM);

            // ✅ Check UserName uniqueness
            var existingUserName = await _userManager.FindByNameAsync(externalStudentVM.UserName);
            if (existingUserName != null)
            {
                ModelState.AddModelError("UserName", "Username is already taken.");
                return View(externalStudentVM);
            }

            // ✅ Check Email uniqueness
            var existingEmail = await _userManager.FindByEmailAsync(externalStudentVM.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(externalStudentVM);
            }

            var user = new ApplicationUser
            {
                UserName = externalStudentVM.UserName,
                Email = externalStudentVM.Email,
                FirstName = externalStudentVM.FirstName,
                LastName = externalStudentVM.LastName,
                FullName = externalStudentVM.FirstName + " " + externalStudentVM.LastName,
                EmailConfirmed = externalStudentVM.EmailConfirmed
            };

            var result = await _userManager.CreateAsync(user, "Pass123+");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, SD.ExternalStudent);

                TempData["success-notification"] = "External Student created successfully.";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(externalStudentVM);
        }


        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var vm = new AdminExternalStudentVM
            {
                Id=user.Id,
                UserName = user!.UserName??"",
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user!.Email??"",
                EmailConfirmed = user.EmailConfirmed
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminExternalStudentVM externalStudentVM)
        {
            var user = await _userManager.FindByIdAsync(externalStudentVM.Id);
            if (user == null) return NotFound();

            if (!ModelState.IsValid)
                return View(externalStudentVM);

            // ✅ Check UserName uniqueness (exclude current user)
            var existingUserName = await _userManager.FindByNameAsync(externalStudentVM.UserName);
            if (existingUserName != null && existingUserName.Id != user.Id)
            {
                ModelState.AddModelError("UserName", "Username is already taken.");
                return View(externalStudentVM);
            }

            // ✅ Check Email uniqueness (exclude current user)
            var existingEmail = await _userManager.FindByEmailAsync(externalStudentVM.Email);
            if (existingEmail != null && existingEmail.Id != user.Id)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(externalStudentVM);
            }

            user.UserName = externalStudentVM.UserName;
            user.FirstName = externalStudentVM.FirstName;
            user.LastName = externalStudentVM.LastName;
            user.FullName = externalStudentVM.FirstName + " " + externalStudentVM.LastName;
            user.Email = externalStudentVM.Email;
            user.EmailConfirmed = externalStudentVM.EmailConfirmed;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["success-notification"] = "External Student updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(externalStudentVM);
        }

        public async Task<IActionResult> Delete(string id)
        {

            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);
          
            TempData["success-notification"] = "User deleted successfully!";

            return RedirectToAction(nameof(Index), "ExternalStudent", new { area = "Admin" });
        }

    }
}
