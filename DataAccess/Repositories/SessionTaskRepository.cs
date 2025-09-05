using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class SessionTaskRepository : Repository<SessionTask>, ISessionTaskRepository
    {
        private readonly ApplicationDbContext _context;
        public SessionTaskRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
