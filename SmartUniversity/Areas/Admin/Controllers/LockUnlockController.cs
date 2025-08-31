using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LockUnlockController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public LockUnlockController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> ToggleBlock(string id, string returnController)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            if (user.LockoutEnd is not null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
                TempData["success-notification"] = "User has been unblocked successfully.";
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddDays(30);
                TempData["success-notification"] = "User has been blocked successfully.";
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", returnController, new { area = "Admin" });
        }
    }
}
