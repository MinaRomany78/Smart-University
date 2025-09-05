using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class CourseSessionRepository : Repository<CourseSession>, ICourseSessionRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseSessionRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
