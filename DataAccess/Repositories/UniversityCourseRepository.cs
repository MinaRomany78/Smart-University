using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UniversityCourseRepository : Repository<UniversityCourse>, IUniversityCourseRepository
    {
        private readonly ApplicationDbContext _context;
        public UniversityCourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UniversityCourse?> GetCourseWithDetailsAsync(int courseId)
        {
            return await _context.UniversityCourses
                .Include(c => c.Doctor)
                    .ThenInclude(d => d.ApplicationUser)
                .Include(c => c.AssistantCourses)
                    .ThenInclude(ac => ac.Assistant)
                        .ThenInclude(a => a.ApplicationUser)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }
    }
}
