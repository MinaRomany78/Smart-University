using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


            var enrollments = await _unitOfWork.Enrollments.GetAsync(include : new Expression<Func<Enrollment, object>>[]
            {
                e => e.UniversityCourse,
                e => e.UniversityCourse.Department
            });
            var enrollmentGrowth = enrollments
                .GroupBy(e => new { e.EnrollmentDate.Year, e.EnrollmentDate.Month })
                .Select(g => new {
                    Period = $"{g.Key.Month}/{g.Key.Year}",
                    Count = g.Count(),
                    Paid = g.Count(x => x.IsPaid),
                    NotPaid = g.Count(x => !x.IsPaid)
                }).OrderBy(e => e.Period).ToList();

            ViewBag.enrollmentGrowth = enrollmentGrowth;


            var deptDist = enrollments
                .GroupBy(e => e.UniversityCourse.Department.Name)
                .Select(g => new { Dept = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.deptDist = deptDist;

            var revenueByMonth = await _unitOfWork.Orders.GetAsync(e => e.OptionalCourseId != null);

            var revenueGrouped = revenueByMonth
                .GroupBy(o => new { o.CreatedAt.Year, o.CreatedAt.Month })
                .Select(g => new {
                    Period = $"{g.Key.Month}/{g.Key.Year}",
                    TotalRevenue = g.Sum(o => o.PricePaid)
                })
                .OrderBy(r => r.Period)
                .ToList();

            ViewBag.revenueByMonth = revenueGrouped;

            return View();
        }
    }
}
