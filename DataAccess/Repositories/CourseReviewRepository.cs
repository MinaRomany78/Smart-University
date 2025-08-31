using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class CourseReviewRepository : Repository<CourseReview>, ICourseReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseReviewRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
