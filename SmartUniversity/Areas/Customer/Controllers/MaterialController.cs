using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("customer")]
    public class MaterialController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaterialController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Lecture(int CourseId, string color)
        {
            if (CourseId == 0) { return NotFound(); }
             var lectures =await _unitOfWork.Materials.GetAsync(e=>e.UniversityCourseID == CourseId &&e.MaterialType==MaterialType.Lecture);
            if (lectures == null) { return NotFound(); }
            ViewData["CourseId"] = CourseId;
            ViewData["ActiveTab"] = "Lectures";
            ViewData["Color"] = color;
            return View(lectures);
        }
        public async Task<IActionResult> Lab(int CourseId, string color)
        {
            if (CourseId == 0) { return NotFound(); }
            var Labs = await _unitOfWork.Materials.GetAsync(e => e.UniversityCourseID == CourseId && e.MaterialType == MaterialType.Lab);
            if (Labs == null) { return NotFound(); }
            ViewData["CourseId"] = CourseId;
            ViewData["ActiveTab"] = "Labs";
            ViewData["Color"] = color;
            return View(Labs);
        }
    }
}
