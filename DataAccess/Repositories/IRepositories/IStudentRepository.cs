using Entities.Models;

using System.Threading.Tasks;

namespace DataAccess.Repositories.IRepositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        //Task<Student?> GetByNationalIdAsync(string nationalId); // واجهة مفيدة للبحث
    }
}
