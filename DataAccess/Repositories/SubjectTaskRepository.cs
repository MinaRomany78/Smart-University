using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class SubjectTaskRepository : Repository<SubjectTask>, ISubjectTaskRepository
    {
        private readonly ApplicationDbContext _context;
        public SubjectTaskRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
