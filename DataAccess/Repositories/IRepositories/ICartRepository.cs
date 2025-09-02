using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IRepositories
{
    public interface ICartRepository : IRepository<Cart>
    {Task RemoveRangeAsync(IEnumerable<Cart> carts);

       // void RemoveRange(IEnumerable<Cart> carts);
    }
}
