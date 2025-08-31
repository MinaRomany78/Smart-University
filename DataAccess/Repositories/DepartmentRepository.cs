using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
