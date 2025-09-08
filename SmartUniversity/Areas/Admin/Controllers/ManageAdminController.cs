using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin}")]
    public class ManageAdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ManageAdminController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            var admins = new List<ApplicationUser>();

            foreach (var admin in users)
            {
                var roles = await _userManager.GetRolesAsync(admin);

                if (roles.Contains(SD.Admin) || roles.Contains(SD.SuperAdmin))
                    admins.Add(admin);
            }

            return View(admins);
        }

        public IActionResult Create()
        {
            var roles = new List<string> { SD.Admin, SD.SuperAdmin, SD.ExternalStudent, SD.UniversityStudent};
            ViewBag.Roles = new SelectList(roles);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManageAdminVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Email = vm.Email,
                    FullName = vm.FullName,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, "Admin123+");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, vm.Role);
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            ViewBag.Roles = new SelectList(new List<string> { SD.Admin, SD.SuperAdmin });

            return View(vm);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var vm = new ManageAdminVM
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? "",
                Role = roles.FirstOrDefault(e => e == SD.Admin || e == SD.SuperAdmin) ?? SD.Admin,
                IsEmailConfirmed = user.EmailConfirmed
            };

            ViewBag.Roles = new SelectList(new List<string> { SD.Admin, SD.SuperAdmin }, vm.Role);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManageAdminVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(new List<string> { SD.Admin, SD.SuperAdmin }, vm.Role);
                return View(vm);
            }

            var user = await _userManager.FindByIdAsync(vm.Id!);
            if (user == null)
                return NotFound();

            user.FullName = vm.FullName;
            user.Email = vm.Email;
            user.UserName = vm.Email;
            user.EmailConfirmed = vm.IsEmailConfirmed;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                ViewBag.Roles = new SelectList(new List<string> { SD.Admin, SD.SuperAdmin }, vm.Role);
                return View(vm);
            }

            var existingRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, existingRoles);

            await _userManager.AddToRoleAsync(user, vm.Role);

            return RedirectToAction(nameof(Index));
        }
    }
}