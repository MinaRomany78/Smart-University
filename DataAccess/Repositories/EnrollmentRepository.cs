using DataAccess.Data;
using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
        public async Task<IEnumerable<Enrollment>> GetEnrollmentsWithDetailsAsync(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentID == studentId)
                .Include(e => e.UniversityCourse)
                    .ThenInclude(c => c.Doctor)
                        .ThenInclude(d => d.ApplicationUser)
                .Include(e => e.UniversityCourse)
                    .ThenInclude(c => c.AssistantCourses)   
                        .ThenInclude(ac => ac.Assistant)
                            .ThenInclude(a => a.ApplicationUser)
                .ToListAsync();
        }

    }
}
