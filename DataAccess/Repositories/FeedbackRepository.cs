using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;
        public FeedbackRepository(ApplicationDbContext context) : base(context) { 
            _context = context;
        }
    }
}
