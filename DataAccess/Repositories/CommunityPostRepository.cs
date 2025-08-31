using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CommunityPostRepository : Repository<CommunityPost>, ICommunityPostRepository
    {
        private readonly ApplicationDbContext _context;
        public CommunityPostRepository(ApplicationDbContext context) : base(context) { 
            _context = context;
        }
        public async Task<List<CommunityPost>> GetPostsByCourseAsync(int courseId)
        {
            return await _context.CommunityPosts
                .Where(p => p.CourseID == courseId)
                .Include(p => p.Author)
                .Include(p => p.Files) // ✅ إضافة الملفات
                .Include(p => p.Links) // ✅ إضافة الروابط
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Author)
                .OrderByDescending(p => p.PostDate)
                .ToListAsync();
        }

        public async Task<CommunityPost?> GetPostWithDetailsAsync(int postId)
        {
            return await _context.CommunityPosts
                .Include(p => p.Author)
                .Include(p => p.Files) // ✅ إضافة الملفات
                .Include(p => p.Links) // ✅ إضافة الروابط
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }



    }
}
