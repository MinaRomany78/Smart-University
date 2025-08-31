using Entities.Models;

using System.Threading.Tasks;

namespace DataAccess.Repositories.IRepositories
{
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {
        //Task<PromoCode?> GetByCodeAsync(string code); // واجهة مفيدة لـ PromoCode
    }
}
