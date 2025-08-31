using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class OptionalCourseRepository : Repository<OptionalCourse>, IOptionalCourseRepository
    {
        private readonly ApplicationDbContext _context;
        public OptionalCourseRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
