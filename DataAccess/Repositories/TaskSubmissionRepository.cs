using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class TaskSubmissionRepository : Repository<TaskSubmission>, ITaskSubmissionRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskSubmissionRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
