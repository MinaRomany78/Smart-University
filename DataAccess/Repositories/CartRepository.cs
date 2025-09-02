using DataAccess.Data;
using Entities.Models;

namespace DataAccess.Repositories.IRepositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task RemoveRangeAsync(IEnumerable<Cart> carts)
        {
            _context.Carts.RemoveRange(carts);
            await Task.CompletedTask;
        }

    }
}
