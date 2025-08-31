using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApplicationController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var applications = await _unitOfWork.Applications.GetAsync();

            if (applications == null)
                return NotFound();

            var totalApplicationsInPage = 15;
            var totalPages = Math.Ceiling((double)applications.Count() / totalApplicationsInPage);

            if (page > totalPages && totalPages != 0)
                return View();

            applications = applications
                .Skip((page - 1) * (int)totalApplicationsInPage)
                .Take((int)totalApplicationsInPage);

            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(applications);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminApplicationVM applicationVM)
        {
            if (!ModelState.IsValid) { 
                return View(applicationVM);
            }
            var newApplication = new Application
            {
                Name = applicationVM.Name,
                NationalID = applicationVM.NationalId,
                Email = applicationVM.Email,
                Age = applicationVM.Age,
                Gender = applicationVM.Gender,
                Address = applicationVM.Address,
                GenerateEmail = false
            };

            await _unitOfWork.Applications.CreateAsync(newApplication);
            await _unitOfWork.Applications.CommitAsync();

            TempData["success-notification"] = "Application Created successfully!";


            return RedirectToAction(nameof(Index), "Application", new { area = "Admin" });
        }
        [HttpGet]
 
        public async Task<IActionResult> Edit(int id)
        {
            var applicationData = await _unitOfWork.Applications.GetOneAsync(e => e.Id == id);
            if (applicationData == null)
            {
                return NotFound();
            }

            var applicationVM = new AdminApplicationVM
            {
                Id = applicationData.Id,
                Name = applicationData.Name,
                NationalId = applicationData.NationalID,
                Email = applicationData.Email,
                Age = applicationData.Age,
                Gender = applicationData.Gender,
                Address = applicationData.Address
            };

            return View(applicationVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminApplicationVM applicationVM)
        {
            if (!ModelState.IsValid)
            {
                return View(applicationVM);
            }

            var applicationUpdate = await _unitOfWork.Applications.GetOneAsync(e => e.Id == applicationVM.Id);
            if (applicationUpdate == null)
            {
                return NotFound();
            }

            applicationUpdate.Name = applicationVM.Name;
            applicationUpdate.NationalID = applicationVM.NationalId;
            applicationUpdate.Email = applicationVM.Email;
            applicationUpdate.Age = applicationVM.Age;
            applicationUpdate.Gender = applicationVM.Gender;
            applicationUpdate.Address = applicationVM.Address;

            await _unitOfWork.Applications.UpdateAsync(applicationUpdate);
            await _unitOfWork.Applications.CommitAsync();
            TempData["success-notification"] = "Application Update successfully!";

            return RedirectToAction(nameof(Index), "Application", new { area = "Admin" });
        }


        public async Task<IActionResult> Delete(int id)
        {
            var applicationRemove = await _unitOfWork.Applications.GetOneAsync(e => e.Id == id);
            if (applicationRemove == null)
                return NotFound();

            await _unitOfWork.Applications.DeleteAsync(applicationRemove);
            await _unitOfWork.Applications.CommitAsync();

            TempData["success-notification"] = "Application deleted successfully!";

            return RedirectToAction(nameof(Index), "Application", new { area = "Admin" });
        }





    }
}
