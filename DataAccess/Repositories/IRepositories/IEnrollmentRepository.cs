using Entities.Models;


namespace DataAccess.Repositories.IRepositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment> {
        Task<IEnumerable<Enrollment>> GetEnrollmentsWithDetailsAsync(int studentId);
    }

}
