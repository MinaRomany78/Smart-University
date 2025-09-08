using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = $"{SD.UniversityStudent},{SD.ExternalStudent},{SD.Instructor}")]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public CartController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            var cartItems = await _unitOfWork.Carts.GetAsync(c => c.ApplicationUserId == user.Id,
                include: new Expression<Func<Cart, object>>[]
                {
                 e => e.OptionalCourse
                }
            );

            var vm = new CartVM();

            foreach (var item in cartItems)
            {
                var originalPrice = item.OptionalCourse.Price;
                var discountValue = item.DiscountPercentage ?? 0m; // safe
                var finalPrice = originalPrice - (originalPrice * discountValue);

                vm.Items.Add(new CartItemVM
                {
                    CourseId = item.OptionalCourseId,
                    CourseName = item.OptionalCourse.Name,
                    OriginalPrice = originalPrice,
                    FinalPrice = finalPrice,
                    AppliedPromoCode = item.AppliedPromoCode,
                    DiscountPercentage = discountValue
                });

                vm.TotalPrice += finalPrice;
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, string? promoCode, string? actionType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
                return NotFound();

            var student = await _unitOfWork.Students.GetOneAsync(s => s.ApplicationUserId == user.Id);
            var oCourse = await _unitOfWork.OptionalCourses.GetOneAsync(c => c.Id == id);

            if (oCourse == null)
                return NotFound();

            decimal? discount = null;
            PromoCode? appliedCode = null;

            // ✅ PromoCode
            if (!string.IsNullOrEmpty(promoCode))
            {
                appliedCode = await _unitOfWork.PromoCodes.GetOneAsync(p => p.Code == promoCode);

                if (appliedCode == null)
                {
                    TempData["error-notification"] = "Invalid promo code.";
                    return RedirectToAction("Details", "ExternalStudent", new { id });
                }
                else if (appliedCode.IsForUniversityStudentsOnly && (student == null || !student.IsUniversityStudent))
                {
                    TempData["error-notification"] = "This promo code is only for university students.";
                    return RedirectToAction("Details", "ExternalStudent", new { id });
                }

                discount = appliedCode.DiscountPercent / 100m;
            }

            // ✅ External Student → أول عملية = 30% خصم
            var orders = await _unitOfWork.Orders.GetAsync(o => o.ApplicationUserId == user.Id);
            var hasPreviousOrder = orders.Any();
            if ((student == null || !student.IsUniversityStudent) && !hasPreviousOrder)
            {
                discount = (discount ?? 0m) + 0.30m;
                if (discount > 1m) discount = 1m;
            }

            var discountToApply = discount ?? 0m;

            var courseInCart = await _unitOfWork.Carts.GetOneAsync(e => e.ApplicationUserId == user.Id && e.OptionalCourseId == id);
            if (courseInCart == null)
            {
                var cartItem = new Cart
                {
                    ApplicationUserId = user.Id,
                    OptionalCourseId = id,
                    AppliedPromoCode = appliedCode?.Code,
                    DiscountPercentage = discountToApply
                };

                await _unitOfWork.Carts.CreateAsync(cartItem);
                await _unitOfWork.Carts.CommitAsync();

                TempData["success-notification"] = "Course added to cart successfully!";
            }
            else
            {
                TempData["info-notification"] = "Course is already in your cart.";
            }

            if (actionType == "buy")
            {
                return RedirectToAction("PayNow", "Cart", new { area = "Customer", courseId = id });
            }


            return RedirectToAction("Details", "ExternalStudent", new { id });
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is not null)
            {
                var courseInCart = await _unitOfWork.Carts.GetOneAsync(e => e.ApplicationUserId == user.Id && e.OptionalCourseId == id);
                if (courseInCart is not null)
                {
                    await _unitOfWork.Carts.DeleteAsync(courseInCart);
                    TempData["success-notification"] = "Removed from Cart successfully";
                    return RedirectToAction(actionName: "Index");
                }
                return NotFound();
            }
            return NotFound();
        }

        public async Task<IActionResult> PayNow(int? courseId = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            // لو جاي من Buy Now → يبقى courseId موجود
            IEnumerable<Cart> cartItems;
            if (courseId.HasValue)
            {
                var singleItem = await _unitOfWork.Carts.GetOneAsync(
                    c => c.ApplicationUserId == user.Id && c.OptionalCourseId == courseId.Value,
                    include: new Expression<Func<Cart, object>>[]
                    {
                e => e.OptionalCourse
                    }
                );

                if (singleItem == null)
                {
                    TempData["error-notification"] = "This course is not in your cart.";
                    return RedirectToAction("Details", "ExternalStudent", new { id = courseId.Value });
                }

                cartItems = new List<Cart> { singleItem };
            }
            else
            {
                cartItems = await _unitOfWork.Carts.GetAsync(
                    c => c.ApplicationUserId == user.Id,
                    include: new Expression<Func<Cart, object>>[]
                    {
                e => e.OptionalCourse
                    }
                );

                if (cartItems == null || !cartItems.Any())
                {
                    TempData["error-notification"] = "You do not have any courses to pay for.";
                    return RedirectToAction(nameof(Index));
                }
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/Customer/Checkout/SuccessOptionalCourses?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/Customer/Checkout/Cancel",
            };

            foreach (var item in cartItems)
            {
                var originalPrice = item.OptionalCourse.Price;
                var discount = item.DiscountPercentage ?? 0m;
                var finalPrice = originalPrice - (originalPrice * discount);

                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        UnitAmount = (long)(finalPrice * 100), 
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.OptionalCourse.Name
                        }
                    },
                    Quantity = 1
                });
            }

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }



    }
}
