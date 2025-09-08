using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var Departments = await _unitOfWork.Departments.GetAsync();
            return View(Departments);
            
        }
        public IActionResult Create() { 
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Create(AdminDepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVM); 
            }
            var department = new Department
            {
                Name = departmentVM.Name,
            };

            await _unitOfWork.Departments.CreateAsync(department);
            await _unitOfWork.Departments.CommitAsync();
            TempData["success-notification"] = "Department Created successfully!";

            return RedirectToAction(nameof(Index)); 
        }
        public async Task<IActionResult> Edit(int id) { 
            var depart=await _unitOfWork.Departments.GetOneAsync(e=>e.Id==id);
            if (depart == null)
                return NotFound();
            var departmentVM = new AdminDepartmentVM
            {
                Id = id,
                Name = depart.Name,
            };
            return View(departmentVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AdminDepartmentVM departmentVM) {
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var department= await _unitOfWork.Departments.GetOneAsync(e => e.Id == departmentVM.Id);
            if (department == null)
                return NotFound();
            department.Name = departmentVM.Name;
            await _unitOfWork.Departments.UpdateAsync(department);
            await _unitOfWork.Departments.CommitAsync();
            TempData["success-notification"] = "Department Update successfully!";

            return RedirectToAction(nameof(Index), "Department", new { area = "Admin" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            var departmentsRemove = await _unitOfWork.Departments.GetOneAsync(e => e.Id == id);
            if (departmentsRemove == null)
                return NotFound();

            await _unitOfWork.Departments.DeleteAsync(departmentsRemove);
            await _unitOfWork.Departments.CommitAsync();

            TempData["success-notification"] = "Department deleted successfully!";

            return RedirectToAction(nameof(Index), "Department", new { area = "Admin" });
        }
    }

}

