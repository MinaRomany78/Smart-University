using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageAdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public ManageAdminController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
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
    }
}