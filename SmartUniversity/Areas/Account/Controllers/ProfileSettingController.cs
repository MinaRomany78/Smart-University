using Entities.Models;
using Entities.ViewModel;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Account.Controllers
{
    [Area("Account")]
    public class ProfileSettingController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileSettingController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if ( user is null)
                return NotFound();


            return View(user);
        }


        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);

            var editProfile = user.Adapt<EditProfileVM>();

            if (user is null)
                return NotFound();

            return View(editProfile);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileVM editProfileVM)
        {
            if (!ModelState.IsValid)
                return View(editProfileVM);

            var user = await _userManager.FindByIdAsync(editProfileVM.Id);

            if (user is null)
                return NotFound();

            user.FirstName = editProfileVM.FirstName;
            user.LastName = editProfileVM.LastName;
            user.FullName = $"{editProfileVM.FirstName} {editProfileVM.LastName}";
            user.UserName = editProfileVM.UserName;
            user.Email = editProfileVM.Email;
            user.Address = editProfileVM.Address;

            await _userManager.UpdateAsync(user);

            TempData["success-notification"] = "Data Updated Successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ChPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChPassword(ChangePasswordProfileVM chPassword)
        {
            if (!ModelState.IsValid)
                return View(chPassword);

            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            var result = await _userManager.ChangePasswordAsync(user, chPassword.CurrentPassword, chPassword.NewPassword);

            if (result.Succeeded)
            {
                TempData["success-notification"] = "Password Changed Successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(chPassword);
        }
    }
}
