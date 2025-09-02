
using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UniversityStudentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UniversityStudentController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
      
        public async Task<IActionResult> RegisterCourses()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var student = await _unitOfWork.Students.GetOneAsync(e => e.ApplicationUserId == user.Id);
            if (student == null)
                return NotFound();

            var paidEnrollments = await _unitOfWork.Enrollments.GetAsync(e => e.StudentID == student.Id && e.IsPaid && e.UniversityCourse.TermId == student.TermId);
            if (paidEnrollments.Any())
            {
                ViewBag.NoCoursesMessage = "You have already registered and paid. No courses are available for registration.";
                return View(new List<RegisterCoursesVM>()); 
            }

            var courses = await _unitOfWork.UniversityCourses.GetAsync(
                e => e.TermId == student.TermId && e.DepartmentID == student.DepartmentID
            );

            var registered = await _unitOfWork.Enrollments.GetAsync(
                    e => e.StudentID == student.Id && e.UniversityCourse.TermId == student.TermId
                );

            var registeredIds = registered.Select(e => e.UniversityCourseID).ToList();
            int alreadyRegisteredCredits = registered.Sum(e => e.CreditHours);

            var vm = courses.Select(c => new RegisterCoursesVM
            {
                CourseId = c.Id,
                Name = c.Name,
                Credits = c.CreditHours,
                IsSelected = registeredIds.Contains(c.Id)
            }).ToList();

            ViewBag.AlreadyRegisteredCredits = alreadyRegisteredCredits;

            return View(vm);
        }



        [HttpPost]
        public async Task<IActionResult> RegisterCourses(List<int> SelectedCourses)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var student = await _unitOfWork.Students.GetOneAsync(s => s.ApplicationUserId == user.Id);
            if (student == null) return NotFound();

            if (SelectedCourses == null || !SelectedCourses.Any())
            {
                TempData["error-notification"] = "Please select at least one course.";
                return RedirectToAction(nameof(RegisterCourses));
            }

            SelectedCourses = SelectedCourses.Distinct().ToList();

            var registered = await _unitOfWork.Enrollments.GetAsync(
                e => e.StudentID == student.Id && e.UniversityCourse.TermId == student.TermId
            );

            var registeredIds = registered.Select(e => e.UniversityCourseID).ToList();
            int alreadyRegisteredCredits = registered.Sum(e => e.CreditHours);

            var newCourses = await _unitOfWork.UniversityCourses.GetAsync(
                c => SelectedCourses.Contains(c.Id) && !registeredIds.Contains(c.Id)
            );

            int newCredits = newCourses.Sum(c => c.CreditHours);
            int totalCredits = alreadyRegisteredCredits + newCredits;

            if (totalCredits < 15 || totalCredits > 18)
            {
                TempData["error-notification"] = "You must register between 15 and 18 credit hours in total.";
                return RedirectToAction(nameof(RegisterCourses));
            }

            foreach (var course in newCourses)
            {
                var enrollment = new Enrollment
                {
                    StudentID = student.Id,
                    UniversityCourseID = course.Id,
                    EnrollmentDate = DateTime.Now,
                    IsPaid = false,
                    Term = course.Term?.Name ?? "N/A",
                    CreditHours = course.CreditHours
                };

                await _unitOfWork.Enrollments.CreateAsync(enrollment);
            }

            TempData["success-notification"] = "Courses registered successfully.";
            return RedirectToAction(nameof(CartOfCourses));
        }


        public async Task<IActionResult> CartOfCourses()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var student = await _unitOfWork.Students.GetOneAsync(s => s.ApplicationUserId == user.Id);
            if (student == null) return NotFound();

            var enrollments = await _unitOfWork.Enrollments.GetAsync(
                e => e.StudentID == student.Id&&e.IsPaid==false,

                include: new Expression<Func<Enrollment, object>>[] { e => e.UniversityCourse }
            );

            var vm = enrollments.Select(e => new RegisterCoursesVM
            {
                CourseId = e.UniversityCourseID,
                Name = e.UniversityCourse.Name,
                Credits = e.CreditHours,
            }).ToList();

            var totalCredits = vm.Sum(c => c.Credits);

            if (totalCredits < 15)
            {
                ViewData["error-notification"] = "You must register at least 15 credit hours.";
                return View(vm);
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Unregister(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var student = await _unitOfWork.Students.GetOneAsync(s => s.ApplicationUserId == user.Id);
            if (student == null) return NotFound();

            var enrollment = await _unitOfWork.Enrollments.GetOneAsync(
                e => e.StudentID == student.Id && e.UniversityCourseID == courseId
            );

            if (enrollment != null)
            {
                await _unitOfWork.Enrollments.DeleteAsync(enrollment);


                TempData["success-notification"] = "Course unregistered successfully.";
            }
            else
            {
                TempData["error-notification"] = "Course not found in your registration.";
            }

            return RedirectToAction(nameof(CartOfCourses));
        }

        public async Task<IActionResult> Pay()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var student = await _unitOfWork.Students.GetOneAsync(s => s.ApplicationUserId == user.Id);
            if (student == null) return NotFound();

            var enrollments = await _unitOfWork.Enrollments.GetAsync(
                e => e.StudentID == student.Id,
                include: new Expression<Func<Enrollment, object>>[] { e => e.UniversityCourse }
            );

            if (enrollments == null || !enrollments.Any())
            {
                TempData["error-notification"] = "You do not have any courses to pay for.";
                return RedirectToAction(nameof(CartOfCourses));
            }

            const int pricePerCredit = 50;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/Customer/Checkout/Success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/Customer/Checkout/Cancel",
            };

            foreach (var item in enrollments)
            {
                var courseTotalPrice = item.CreditHours * pricePerCredit;

                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.UniversityCourse.Name,
                            Description = $"Credits: {item.CreditHours}"
                        },
                        UnitAmount = (long)(courseTotalPrice * 100), 
                    },
                    Quantity = 1,
                });
            }

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        public async Task<IActionResult> MyEnrollments()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var student = await _unitOfWork.Students.GetOneAsync(s => s.ApplicationUserId == user.Id);
            if (student == null) return NotFound();

            var enrollments = await _unitOfWork.Enrollments.GetEnrollmentsWithDetailsAsync(student.Id);
           
            var vm = enrollments.Select(e => new EnrollmentVM
            {
                CourseId = e.UniversityCourseID,
                CourseName = e.UniversityCourse.Name,
                DoctorName = e.UniversityCourse.Doctor.ApplicationUser.FullName,

                Assistants = e.UniversityCourse.AssistantCourses
                   .Select(ac => ac.Assistant.ApplicationUser?.FullName ?? "Unknown Assistant")
                   .Distinct() 
                   .ToList()
            }).ToList();

            return View(vm);

        }





    }
}
