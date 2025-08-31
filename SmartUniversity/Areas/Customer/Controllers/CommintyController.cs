using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUniversity.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CommintyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommintyController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int courseId, string color)
        {
            var course = await _unitOfWork.UniversityCourses.GetCourseWithDetailsAsync(courseId);

            if (course == null)
                return NotFound();

            var posts = await _unitOfWork.CommunityPosts.GetPostsByCourseAsync(courseId);

            var vm = new CommunityVM
            {
                Course = course,
                Posts = posts,
                Color = color,
                Assistants = course.AssistantCourses
                    .Select(ac => ac.Assistant.ApplicationUser?.FullName ?? "Unknown Assistant")
                    .Distinct()
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(CreatePostVM vm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var post = new CommunityPost
            {
                Content = vm.Content,
                PostDate = DateTime.Now,
                AuthorId = user.Id,
                CourseID = vm.CourseId,
            };

            await _unitOfWork.CommunityPosts.CreateAsync(post);
            await _unitOfWork.CommunityPosts.CommitAsync();

            // 🔹 إنشاء مجلد uploads إذا لم يكن موجود
            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            // 🔹 رفع الملفات
            if (vm.Files != null && vm.Files.Any())
            {
                foreach (var file in vm.Files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(uploadDir, fileName);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var postFile = new PostFile
                        {
                            PostId = post.Id,
                            FilePath = "/uploads/" + fileName
                        };

                        await _unitOfWork.PostFiles.CreateAsync(postFile);
                    }
                }
                await _unitOfWork.PostFiles.CommitAsync();
            }

            // 🔹 إضافة الرابط إذا موجود
            if (!string.IsNullOrEmpty(vm.Link))
            {
                var postLink = new PostLink
                {
                    PostId = post.Id,
                    Url = vm.Link
                };
                await _unitOfWork.PostLinks.CreateAsync(postLink);
                await _unitOfWork.PostLinks.CommitAsync();
            }

            TempData["success-notification"] = "Post created successfully!";
            return RedirectToAction("Index", new { courseId = vm.CourseId });
        }
    }
}
