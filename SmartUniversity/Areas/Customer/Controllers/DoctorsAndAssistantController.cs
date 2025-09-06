using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class DoctorsAndAssistantController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorsAndAssistantController(UserManager<ApplicationUser> userManger, IUnitOfWork unitOfWork)
        {
            _userManger = userManger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(SD.Doctor))
                return RedirectToAction("DoctorCourse");
            else if (User.IsInRole(SD.Assistant))
                return RedirectToAction("AssistantCourse");
            return NotFound();
        }

        public async Task<IActionResult> DoctorCourse()
        {
            var user = await _userManger.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account", new { area = "Identity" });

            var doctor = await _unitOfWork.Doctors.GetOneAsync(e => e.ApplicationUserId == user.Id);
            if (doctor == null) return NotFound();

            var courses = await _unitOfWork.UniversityCourses.GetAsync(
                e => e.DoctorID == doctor.Id,
                include: new Expression<Func<UniversityCourse, object>>[]
                {
                    e => e.Term,
                    e => e.Department
                });
            return View(courses);
        }

        public async Task<IActionResult> AssistantCourse()
        {
            var user = await _userManger.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account", new { area = "Identity" });

            var assistant = await _unitOfWork.Assistants.GetOneAsync(e => e.ApplicationUserId == user.Id);
            if (assistant == null) return NotFound();

            var courses = await _unitOfWork.AssistantCourses.GetAsync(
                e => e.AssistantId == assistant.Id,
                include: new Expression<Func<AssistantCourse, object>>[]
                {
                    e => e.Course,
                    e => e.Course.Department,
                    e => e.Course.Term
                });
            return View(courses);
        }

        [HttpGet]
        public IActionResult AddMaterial(int courseId)
        {
            var model = new MaterialVM { UniversityCourseID = courseId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMaterial(MaterialVM materialVM)
        {
            if (!ModelState.IsValid) return View(materialVM);

            var user = await _userManger.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account", new { area = "Identity" });

            var material = new Material
            {
                MaterialLink = materialVM.MaterialLink,
                UniversityCourseID = materialVM.UniversityCourseID,
                UploadDate = DateTime.Now
            };

            if (User.IsInRole(SD.Doctor))
            {
                var doctor = await _unitOfWork.Doctors.GetOneAsync(d => d.ApplicationUserId == user.Id);
                material.DoctorId = doctor.Id;
                material.MaterialType = MaterialType.Lecture;

                await _unitOfWork.Materials.CreateAsync(material);
                TempData["success-notification"] = "Lecture added successfully!";
                return RedirectToAction("Lecture", "Material", new { area = "Customer", CourseId = material.UniversityCourseID, color = "#0d6efd" });
            }
            else if (User.IsInRole(SD.Assistant))
            {
                var assistant = await _unitOfWork.Assistants.GetOneAsync(a => a.ApplicationUserId == user.Id);
                material.AssistantId = assistant.Id;
                material.MaterialType = MaterialType.Lab;

                await _unitOfWork.Materials.CreateAsync(material);
                TempData["success-notification"] = "Lab added successfully!";
                return RedirectToAction("Lab", "Material", new { area = "Customer", CourseId = material.UniversityCourseID, color = "#0f9d58" });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditMaterial(int id)
        {
            var material = await _unitOfWork.Materials.GetOneAsync(m => m.Id == id);
            if (material == null) return NotFound();

            var model = new MaterialVM
            {
                Id = material.Id,
                MaterialLink = material.MaterialLink,
                UniversityCourseID = material.UniversityCourseID
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditMaterial(MaterialVM materialVM)
        {
            if (!ModelState.IsValid) return View(materialVM);

            var material = await _unitOfWork.Materials.GetOneAsync(m => m.Id == materialVM.Id);
            if (material == null) return NotFound();

            material.MaterialLink = materialVM.MaterialLink;
            material.UniversityCourseID = materialVM.UniversityCourseID;

            await _unitOfWork.Materials.UpdateAsync(material);
            TempData["success-notification"] = "Material updated successfully!";

            // Redirect based on type
            if (material.MaterialType == MaterialType.Lecture)
                return RedirectToAction("Lecture", "Material", new { area = "Customer", CourseId = material.UniversityCourseID, color = "#0d6efd" });
            else
                return RedirectToAction("Lab", "Material", new { area = "Customer", CourseId = material.UniversityCourseID, color = "#0f9d58" });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            var material = await _unitOfWork.Materials.GetOneAsync(m => m.Id == id);
            if (material == null) return NotFound();

            await _unitOfWork.Materials.DeleteAsync(material);
            TempData["success-notification"] = "Material deleted successfully!";

            // Redirect based on type
            if (material.MaterialType == MaterialType.Lecture)
                return RedirectToAction("Lecture", "Material", new { area = "Customer", CourseId = material.UniversityCourseID, color = "#0d6efd" });
            else
                return RedirectToAction("Lab", "Material", new { area = "Customer", CourseId = material.UniversityCourseID, color = "#0f9d58" });
        }
    }
}
