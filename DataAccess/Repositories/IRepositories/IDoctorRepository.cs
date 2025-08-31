using Entities.Models;

namespace DataAccess.Repositories.IRepositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        //Task<Doctor?> GetDoctorWithCoursesAsync(int id);
    }
}
