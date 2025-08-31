using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {

            
            var totalStudents = await _unitOfWork.Students.GetAsync();
            
            ViewBag.totalStudents = totalStudents.Count();

            var totalDoctors = await _unitOfWork.Doctors.GetAsync();
            ViewBag.totalDoctors = totalDoctors.Count();

            var totalAssistants = await _unitOfWork.Assistants.GetAsync();
            ViewBag.totalAssistants = totalAssistants.Count();

            var totalCourses = await _unitOfWork.UniversityCourses.GetAsync();
            ViewBag.totalCourses = totalCourses.Count();

            var totalOptionalCourses = await _unitOfWork.OptionalCourses.GetAsync();
            ViewBag.totalOptionalCourses = totalOptionalCourses.Count();

            var tickets = await _unitOfWork.SupportTickets.GetAsync();

            var topTickets = tickets
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.CreatedDate)
            .Take(5)
            .ToList();

            ViewBag.toptickets = topTickets;
            

            return View();
        }
    }
}
