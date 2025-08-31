using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class ApplicationUserOtpRepository : Repository<ApplicationUserOtp>, IApplicationUserOtpRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserOtpRepository(ApplicationDbContext context) : base(context) { 
            _context = context;
        }
    }
}
