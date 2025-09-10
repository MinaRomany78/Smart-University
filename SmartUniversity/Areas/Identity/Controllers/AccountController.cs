using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

using Utility;
using Utility.DBInitializer;

namespace SmartUniversity.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<ApplicationUser> userManager,IEmailSender emailSender,SignInManager<ApplicationUser> signInManager,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            ApplicationUser user = new()
            {
                UserName = registerVM.UserName,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                FullName = registerVM.FirstName+" "+registerVM.LastName,

                Email = registerVM.Email
            };
            var result= await _userManager.CreateAsync(user,registerVM.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, SD.ExternalStudent);
                TempData["success-notification"] = "User Created successfully!";
              var  token=await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action(nameof(ConfirmEmail), "Account", new { 
                    userId=user.Id,
                    token=token ,
                    area = "Identity" }
                ,Request.Scheme);
               await _emailSender.SendEmailAsync(user.Email,"Confirm your Email", $"<h1>Comfirm your Account by Chilking <a href='{link}'> Here</a></h1>");
                return RedirectToAction("Index", "home", new { area = "Identity" });
            }
            foreach(var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty,item.Description);
            }

            return View();

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            // البحث عن المستخدم بالاسم أو الإيميل
            var user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail)
                       ?? await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);

            if (user == null)
            {
                TempData["error-notification"] = "User Not Found";
                return View(loginVM);
            }

            // التحقق من تأكيد البريد الإلكتروني
            if (!user.EmailConfirmed)
            {
                TempData["error-notification"] = "Confirm Your Account before login!";
                ViewBag.ShowResend = true; // تفعيل زر إعادة إرسال التأكيد
                return View(loginVM);
            }

            // محاولة تسجيل الدخول
            var result = await _signInManager.PasswordSignInAsync(
                user!.UserName??"",
                loginVM.Password,
                loginVM.RememberMe,
                lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                TempData["success-notification"] = "Login Successfully";

                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains($"{SD.UniversityStudent}"))
                    return RedirectToAction("index", "UniversityStudent", new { area = "Customer" });

                if (roles.Contains($"{SD.Doctor}") || roles.Contains($"{SD.Assistant}"))
                    return RedirectToAction("index", "DoctorsAndAssistant", new { area = "Customer" });

                if (roles.Contains($"{SD.ExternalStudent}"))
                    return RedirectToAction("index", "ExternalStudent", new { area = "Customer" });

                return RedirectToAction("index", "Home", new { area = "identity" });
            }

            if (result.IsLockedOut)
            {
                TempData["error-notification"] = $"you have block till {user.LockoutEnd}";
                return View(loginVM);
            }

            ModelState.AddModelError(string.Empty, "Invalid Username or Password");
            return View(loginVM);
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded)
                {
                    TempData["error-notification"] = $"{String.Join(",", result.Errors)}";
                }
                else
                {
                    TempData["success-notification"] = "Email Confirmation Successfully!";
                }
                return RedirectToAction("Index", "Home", new { area = "Identity" });
            }
            return NotFound();

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["success-notification"] = "Logout Successfully";
            return RedirectToAction(nameof(Login), "Account", new { area = "Identity" });
        }
        public IActionResult ResendEmailConfirmation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResendEmailConfirmation(ResendEmailConfirmationVM resendEmailConfirmationVM)
        {
            if (!ModelState.IsValid)
            {
                return View(resendEmailConfirmationVM);
            }

            var user = await _userManager.FindByEmailAsync(resendEmailConfirmationVM.EmailORUserName) ?? await _userManager.FindByNameAsync(resendEmailConfirmationVM.EmailORUserName);

            if (user is not null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action(nameof(ConfirmEmail), "Account", new { area = "Identity", token = token, userId = user.Id }, Request.Scheme);
                await _emailSender.SendEmailAsync(user.Email!, "Confirm Your Account", $"<h1>Confirm Your Account By Clicking <a href='{link}'>Here</a></h1>");
            }

            // Send msg
            TempData["success-notification"] = "Confirm Your Account Again!";
            return RedirectToAction("index", "Home", new { area = "Identity" });
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(forgetPasswordVM);
            }
            var user = await _userManager.FindByEmailAsync(forgetPasswordVM.EmailORUserName) ?? await _userManager.FindByNameAsync(forgetPasswordVM.EmailORUserName);

            var userOTP = await _unitOfWork.ApplicationUserOtps.GetAsync(e => e.ApplicationUserId == user.Id);

            var totalOTPs = userOTP.Count(e => (e.Date.Day == DateTime.UtcNow.Day) && (e.Date.Month == DateTime.UtcNow.Month) && (e.Date.Year == DateTime.UtcNow.Year));

            if (totalOTPs < 5)
            {
                if (user is not null)
                {
                    var OTPNumber = new Random().Next(1000, 9999);
                    await _emailSender.SendEmailAsync(user.Email!, "Reset Password", $"<h1>Reset Password Using OTP Number {OTPNumber}</h1>");

                    await _unitOfWork.ApplicationUserOtps.CreateAsync(new()
                    {
                        OtpNumber = OTPNumber.ToString(),
                        Date = DateTime.UtcNow,
                        ExpirationDate = DateTime.UtcNow.AddHours(1),
                        ApplicationUserId = user.Id,
                        Reason="Forget PassWord"
                    });
                    await _unitOfWork.ApplicationUserOtps.CommitAsync();
                }

                TempData["RedirectToAction"] = Guid.NewGuid().ToString();
                return RedirectToAction(nameof(ResetPassword), new { userId = user.Id! });
            }

            // Send msg
            TempData["error-notification"] = "Too Many Request, Please try again Later";
            return View(forgetPasswordVM);
        }
        public IActionResult ResetPassword(string userId)
        {
            if (TempData["RedirectToAction"] is not null)
            {
                if (userId is not null)
                {
                    return View(new ResetPasswordVM()
                    {
                        UserId = userId
                    });
                }

            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            var userOTP = (await _unitOfWork.ApplicationUserOtps.GetAsync(e => e.ApplicationUserId == resetPasswordVM.UserId)).OrderBy(e => e.Id).LastOrDefault();

            if (userOTP is not null)
            {
                if (DateTime.UtcNow < userOTP.ExpirationDate && !userOTP.Status && userOTP.OtpNumber == resetPasswordVM.Otp)
                {
                    TempData["RedirectToAction"] = Guid.NewGuid().ToString();
                    return RedirectToAction(nameof(ChangePassword), new { userId = userOTP.ApplicationUserId! });
                }
            }

            // Error
            ModelState.AddModelError(string.Empty, "Invalid Code");
            return View(resetPasswordVM);
        }

        public IActionResult ChangePassword(string userId)
        {
            if (TempData["RedirectToAction"] is not null)
            {
                return View(new ChangePasswordVM()
                {
                    UserId = userId
                });
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordVM);
            }

            var user = await _userManager.FindByIdAsync(changePasswordVM.UserId);

            if (user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, changePasswordVM.Password);

                // Send msg
                TempData["success-notification"] = "Reset Password Successfully";
                return RedirectToAction("index", "Home", new { area = "Identity" });
            }

            return NotFound();
        }
    }
    
}
