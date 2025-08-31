using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        private readonly ApplicationDbContext _context;
        public MaterialRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
