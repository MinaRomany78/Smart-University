using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = $"{SD.ExternalStudent},{SD.UniversityStudent},{SD.Instructor}")]
    public class ExternalStudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExternalStudentController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchQuery)
        {
            var courses = await _unitOfWork.OptionalCourses.GetAsync(include: new Expression<Func<OptionalCourse, object>>[]
            {
                e => e.Instructor,
                e => e.Instructor.ApplicationUser
            });

            if (!string.IsNullOrEmpty(searchQuery))
            {
                courses = courses.Where(c =>
                    c.Name.Contains(searchQuery) ||
                    c.Description.Contains(searchQuery) ||
                    c.Instructor.ApplicationUser.FullName.Contains(searchQuery));
            }

            return View(courses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var oCourse = await _unitOfWork.OptionalCourses.GetOneAsync(e => e.Id == id, include: new Expression<Func<OptionalCourse, object>>[]
            {
                e => e.Instructor,
                e => e.Instructor.ApplicationUser,
                e => e.Reviews
            });

            if (oCourse is null)
                return NotFound();

            var topCourses = (await _unitOfWork.OptionalCourses.GetAsync(e => e.Id != id, include: new Expression<Func<OptionalCourse, object>>[]
            {
                e => e.Instructor.ApplicationUser
            }))
                .OrderByDescending(e => e.Traffic).Skip(0).Take(4);

            oCourse.Traffic++;
            await _unitOfWork.OptionalCourses.CommitAsync();

            var reviews = (await _unitOfWork.CourseReviews
            .GetAsync(e => e.CourseId == id,
            include: new Expression<Func<CourseReview, object>>[]
            {
                e => e.ApplicationUser
            }))
            .OrderByDescending(r => r.CreatedAt)
            .ToList();


            var avg = reviews.Any() ? reviews.Average(e => e.Rating) : 0;

            var user = await _userManager.GetUserAsync(User);

            bool hasPurchased = false;
            bool isMyCourse=false;

            if (user is not null)
            {
                if (User.IsInRole($"{SD.ExternalStudent}")|| User.IsInRole($"{SD.UniversityStudent}"))
                {
                    var order = await _unitOfWork.Orders.GetOneAsync(e => e.ApplicationUserId == user.Id
                && e.OptionalCourseId == id);

                    hasPurchased = order is not null;
                }
                else if (User.IsInRole($"{SD.Instructor}"))
                { 
                    var instructor=await _unitOfWork.Instructors.GetOneAsync(e=>e.ApplicationUserId==user.Id);
                    if (instructor is null)
                        return NotFound();
                    var oinstructorCourse = await _unitOfWork.OptionalCourses
                        .GetOneAsync(e => e.Id == id && e.InstructorId == instructor.Id);

                    isMyCourse = oinstructorCourse is not null;

                    var order = await _unitOfWork.Orders.GetOneAsync(e => e.ApplicationUserId == user.Id
                && e.OptionalCourseId == id);

                    hasPurchased = order is not null;

                }
                   
            }

            return View(new OcoursesWithTopANDReviewsVM()
            {
                OptionalCourse = oCourse,
                TopCourses = topCourses,
                Reviews = reviews,
                AverageRating = avg,
                HasPurchased = hasPurchased,
                IsMyCourse = isMyCourse,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{SD.UniversityStudent},{SD.ExternalStudent}")]
        public async Task<IActionResult> AddReview(ReviewFormVM reviewVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    TempData["error-notification"] = "You must be logged in to add a review.";
                    return RedirectToAction("Details", new { id = reviewVM.CourseId });
                }

                var review = new CourseReview
                {
                    CourseId = reviewVM.CourseId,
                    Comment = reviewVM.Comment,
                    Rating = reviewVM.Rating,
                    ApplicationUserId = user.Id,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.CourseReviews.CreateAsync(review);
                await _unitOfWork.CourseReviews.CommitAsync();

                return RedirectToAction("Details", new { id = reviewVM.CourseId });
            }

            return RedirectToAction("Details", new { id = reviewVM.CourseId });
        }

    }
}