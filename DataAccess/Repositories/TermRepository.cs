using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class TermRepository : Repository<Term>, ITermRepository
    {
        private readonly ApplicationDbContext _context;
        public TermRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
