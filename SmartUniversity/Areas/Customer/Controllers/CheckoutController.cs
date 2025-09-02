using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CheckoutController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public CheckoutController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        // ✅ للكورسات الأساسية
        public async Task<IActionResult> Success(string session_id)
        {

            var service = new SessionService();
            var session = service.Get(session_id);

            if (session.PaymentStatus != "paid")
            {
                ViewData["Message"] = "❌ Payment not completed.";
                return View("Cancel");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var student = await _unitOfWork.Students.GetOneAsync(s => s.ApplicationUserId == user.Id);
            if (student == null) return NotFound();

            var enrollments = await _unitOfWork.Enrollments.GetAsync(e => e.StudentID == student.Id);

            if (enrollments != null && enrollments.Any())
            {
                foreach (var enrollment in enrollments)
                {
                    enrollment.IsPaid = true;
                    await _unitOfWork.Enrollments.UpdateAsync(enrollment);
                }
            }

            await _unitOfWork.Enrollments.CommitAsync();

            ViewData["Message"] = "✅ Payment successful! Your courses have been paid.";
            return View();
        }

        // ❌ لو الكاشف اتلغي
        public IActionResult Cancel()
        {
            ViewData["Message"] = "❌ Payment was cancelled. Please try again.";
            return View();
        }
        public async Task<IActionResult> SuccessOptionalCourses(string session_id)
        {
            var service = new SessionService();
            var session = service.Get(session_id);

            if (session.PaymentStatus != "paid")
            {
                ViewData["Message"] = "❌ Payment not completed.";
                return View("CancelOptionalCourses");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var cartItems = await _unitOfWork.Carts.GetAsync(
                c => c.ApplicationUserId == user.Id,
                include: new System.Linq.Expressions.Expression<Func<Cart, object>>[]
                {
                    e => e.OptionalCourse
                }
            );

            if (!cartItems.Any())
            {
                ViewData["Message"] = "⚠️ Your cart is already empty.";
                return View();
            }

            foreach (var item in cartItems)
            {
                var order = new Order
                {
                    ApplicationUserId = user.Id,
                    OptionalCourseId = item.OptionalCourseId,
                    PricePaid = item.OptionalCourse.Price - (item.OptionalCourse.Price * (item.DiscountPercentage ?? 0m)),
                    CreatedAt = DateTime.UtcNow,
                    //StripeSessionId = session.Id
                };

                await _unitOfWork.Orders.CreateAsync(order);

                var userCourse = new UserOptionalCourse
                {
                    ApplicationUserId = user.Id,
                    OptionalCourseId = item.OptionalCourseId,
                    PurchaseDate = DateTime.UtcNow,
                    AppliedPromoCode = item.AppliedPromoCode
                };
                await _unitOfWork.UserOptionalCourses.CreateAsync(userCourse);

                if (!string.IsNullOrEmpty(item.AppliedPromoCode))
                {
                    var promo = await _unitOfWork.PromoCodes.GetOneAsync(p => p.Code == item.AppliedPromoCode);
                    if (promo != null && !promo.IsExpired)
                    {
                        promo.CurrentUsage += 1;
                        await _unitOfWork.PromoCodes.UpdateAsync(promo);
                    }
                }

            }


            await _unitOfWork.Carts.RemoveRangeAsync(cartItems);

            await _unitOfWork.Orders.CommitAsync();
            await _unitOfWork.Carts.CommitAsync();

            ViewData["Message"] = "✅ Payment successful! Your optional courses are now available.";
            return View();
        }
        
    }
}
