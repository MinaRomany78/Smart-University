using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupportTicketController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        public SupportTicketController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index()
        {
            var tickets = await _unitOfWork.SupportTickets.GetAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _unitOfWork.SupportTickets.GetOneAsync(e => e.Id == id);
            if (ticket is null)
                return NotFound();

            return View(ticket);
        }

        public async Task<IActionResult> Reply(int id)
        {
            var ticket = await _unitOfWork.SupportTickets.GetOneAsync(e => e.Id == id);
            if (ticket == null)
                return NotFound();

            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Reply(int id, string adminResponse)
        {
            var ticket = await _unitOfWork.SupportTickets.GetOneAsync(e => e.Id == id);

            if (ticket is null)
                return NotFound();

            ticket.AdminResponse = adminResponse;
            ticket.Status = TicketStatus.Resolved;
            ticket.ResolvedDate = DateTime.UtcNow;

            await _unitOfWork.SupportTickets.UpdateAsync(ticket);
            await _unitOfWork.SupportTickets.CommitAsync();

            await _emailSender.SendEmailAsync(ticket.SenderEmail,
            "Support Ticket Response",
            $"Hello {ticket.SenderName},<br/><br/>We have replied to your ticket:<br/><b>{adminResponse}</b>");

            TempData["success-notification"] = "Reply sent successfully and email delivered";
            return RedirectToAction(nameof(Index));
        }
    }
}
