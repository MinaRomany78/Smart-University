using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class SupportTicketController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public SupportTicketController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Create()
        {
            var model = new SupportTicket();

            if (User.Identity!.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    model.SenderEmail = user.Email??" ";
                    model.SenderName = user.FullName; // لو عندك عمود FullName في ApplicationUser
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(SupportTicket ticket)
        {
            if(ModelState.IsValid)
            {
                var exestingUser = await _unitOfWork.ApplicationUsers.GetOneAsync(e => e.Email == ticket.SenderEmail);
                var exestingApplication = await _unitOfWork.Applications.GetOneAsync(e => e.Email == ticket.SenderEmail);
                if (exestingUser is null && exestingApplication is null)
                {
                    ModelState.AddModelError(nameof(SupportTicket.SenderEmail), "This email is not registered in our system.");
                    return View(ticket);
                }

                ticket.Status = TicketStatus.Open;
                ticket.CreatedDate = DateTime.UtcNow;

                await _unitOfWork.SupportTickets.CreateAsync(ticket);
                await _unitOfWork.SupportTickets.CommitAsync();

                TempData["success-notification"] = "Your ticket has been submitted, we mostly reply within 24 hours";
                return RedirectToAction("Index", "Home", new { area = "Identity" });
            }
            return View(ticket);
        }
    }
}
