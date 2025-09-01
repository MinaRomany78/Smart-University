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

            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            
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
        [HttpPost]
      [Authorize]
        public async Task<IActionResult> AddComment(int postId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                TempData["error-notification"] = "Comment cannot be empty.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var comment = new Comment
            {
                PostID = postId,
                Content = content,
                AuthorId = user.Id,
                DatePosted = DateTime.Now
            };

            await _unitOfWork.Comments.CreateAsync(comment);
            await _unitOfWork.Comments.CommitAsync();

            // عشان نرجع لنفس الكورس اللي فيه البوست
            var post = await _unitOfWork.CommunityPosts.GetOneAsync(e=>e.Id==postId);
                    if (post == null)
                        return NotFound();

            TempData["success-notification"] = "Comment added successfully!";
            return RedirectToAction("Index", new { courseId = post.CourseID});
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var post = await _unitOfWork.CommunityPosts.GetOneAsync(p => p.Id == postId);
            if (post == null) return NotFound();

            if (post.AuthorId != user.Id)
                return Forbid();

            var files = await _unitOfWork.PostFiles.GetAsync(f => f.PostId == postId);
            foreach (var file in files)
            {
                var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
                await _unitOfWork.PostFiles.DeleteAsync(file);
            }

            var links = await _unitOfWork.PostLinks.GetAsync(l => l.PostId == postId);
            foreach (var link in links)
            {
                await _unitOfWork.PostLinks.DeleteAsync(link);
            }

            // مسح الكومنتات المرتبطة
            var comments = await _unitOfWork.Comments.GetAsync(c => c.PostID == postId);
            foreach (var comment in comments)
            {
                await _unitOfWork.Comments.DeleteAsync(comment);
            }

            // في الآخر نمسح البوست نفسه
            await _unitOfWork.CommunityPosts.DeleteAsync(post);
            await _unitOfWork.CommunityPosts.CommitAsync();

            TempData["success-notification"] = "Post deleted successfully!";
            return RedirectToAction("Index", new { courseId = post.CourseID });

        }
            [HttpPost]
            [Authorize]
            public async Task<IActionResult> DeleteComment(int commentId)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Unauthorized();

                var comment = await _unitOfWork.Comments.GetOneAsync(c => c.Id == commentId);
                if (comment == null) return NotFound();

                if (comment.AuthorId != user.Id)
                    return Forbid();

                // 🔹 هات البوست عشان تجيب CourseID
                var post = await _unitOfWork.CommunityPosts.GetOneAsync(p => p.Id == comment.PostID);
                if (post == null) return NotFound();

                await _unitOfWork.Comments.DeleteAsync(comment);
                await _unitOfWork.Comments.CommitAsync();

                TempData["success-notification"] = "Comment deleted successfully!";
                return RedirectToAction("Index", new { courseId = post.CourseID });
            }




        }
    }
