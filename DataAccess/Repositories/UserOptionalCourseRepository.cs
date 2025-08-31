using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class UserOptionalCourseRepository : Repository<UserOptionalCourse>, IUserOptionalCourseRepository
    {
        private readonly ApplicationDbContext _context;
        public UserOptionalCourseRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
