using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PromoCodeRepository : Repository<PromoCode>, IPromoCodeRepository
    {
        private readonly ApplicationDbContext _context;
        public PromoCodeRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        //public async Task<PromoCode?> GetByCodeAsync(string code)
        //{
        //    return await _context.PromoCodes
        //        .Include(p => p.OptionalCourses)
        //        .FirstOrDefaultAsync(p => p.Code == code);
        //}
    }
}
