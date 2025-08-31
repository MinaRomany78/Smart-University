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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupportTicket ticket)
        {
            if(ModelState.IsValid)
            {
                ticket.Status = TicketStatus.Open;
                ticket.CreatedDate = DateTime.UtcNow;

                await _unitOfWork.SupportTickets.CreateAsync(ticket);
                await _unitOfWork.SupportTickets.CommitAsync();

                TempData["success-notification"] = "Your ticket has been submitted, we mostly reply about 24 hours";
                return RedirectToAction("Index", "Home", new { area = "Identity" });
            }
            return View(ticket);
        }
    }
}
