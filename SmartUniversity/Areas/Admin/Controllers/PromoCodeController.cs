using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PromoCodeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PromoCodeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var promoCodes = await _unitOfWork.PromoCodes.GetAsync();

            if (promoCodes is null)
                return NotFound();

            var totalCodesInPage = 15;
            var totalPages = Math.Ceiling((double)promoCodes.Count() / totalCodesInPage);

            if (page > totalPages && totalPages != 0)
                return View();

            promoCodes = promoCodes
                .Skip((page - 1) * (int)totalCodesInPage)
                .Take((int)totalCodesInPage);

            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(promoCodes);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.oCourses = await _unitOfWork.OptionalCourses.GetAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PromoCode promoCode, List<int> selectedCourses)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.oCourses = await _unitOfWork.OptionalCourses.GetAsync();
                return View(promoCode);
            }

            promoCode.CurrentUsage = 0;

            await _unitOfWork.PromoCodes.CreateAsync(promoCode);
            await _unitOfWork.PromoCodes.CommitAsync();

            if (selectedCourses is not null && selectedCourses.Count > 0)
            { 
                foreach (var courseId in selectedCourses)
                {
                    var oCourse = await _unitOfWork.OptionalCourses.GetOneAsync(e => e.Id == courseId);
                    if (oCourse is not null)
                    {
                        oCourse.PromoCode = promoCode.Code;
                        await _unitOfWork.OptionalCourses.UpdateAsync(oCourse);
                    }
                }
                await _unitOfWork.PromoCodes.CommitAsync();
            }

            TempData["success-notification"] = "Promo code created successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var promoCode = await _unitOfWork.PromoCodes.GetOneAsync(e => e.Code == id);

            if (promoCode is null)
                return NotFound();

            var oCourses = await _unitOfWork.OptionalCourses.GetAsync();
            ViewBag.oCourses = oCourses;
            ViewBag.SelectedCourses = oCourses.Where(e => e.PromoCode == id).Select(c => c.Id).ToList();

            return View(promoCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PromoCode promoCode, List<int> selectedCourses)
        {
            if (id != promoCode.Code)
                return NotFound();

            if (!ModelState.IsValid)
            {
                var oCourses = await _unitOfWork.OptionalCourses.GetAsync();
                ViewBag.oCourses = oCourses;
                ViewBag.SelectedCourses = selectedCourses;
                return View(promoCode);
            }

            var existingPromo = await _unitOfWork.PromoCodes.GetOneAsync(e => e.Code == id);
            if (existingPromo == null)
                return NotFound();

            existingPromo.DiscountPercent = promoCode.DiscountPercent;
            existingPromo.IsForUniversityStudentsOnly = promoCode.IsForUniversityStudentsOnly;
            existingPromo.MaxUsage = promoCode.MaxUsage;


            await _unitOfWork.PromoCodes.UpdateAsync(existingPromo);

            var ocourses = await _unitOfWork.OptionalCourses.GetAsync();
            foreach (var course in ocourses)
            {
                if (selectedCourses.Contains(course.Id))
                    course.PromoCode = promoCode.Code;
                else if (course.PromoCode == promoCode.Code)
                    course.PromoCode = null;

                await _unitOfWork.OptionalCourses.UpdateAsync(course);
            }

            await _unitOfWork.PromoCodes.CommitAsync();

            TempData["success-notification"] = "Promo code updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var promoCode = await _unitOfWork.PromoCodes.GetOneAsync(e => e.Code == id);

            if (promoCode is null)
                return NotFound();

            var oCourses = await _unitOfWork.OptionalCourses.GetAsync(e => e.PromoCode == promoCode.Code);
            foreach (var course in oCourses)
            {
                course.PromoCode = null;
                await _unitOfWork.OptionalCourses.UpdateAsync(course);
            }

            await _unitOfWork.PromoCodes.DeleteAsync(promoCode);
            await _unitOfWork.PromoCodes.CommitAsync();

            TempData["success-notification"] = "Promo code deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
