using Entities.Models;


namespace DataAccess.Repositories.IRepositories
{
    public interface IUniversityCourseRepository : IRepository<UniversityCourse>
    {
        Task<UniversityCourse?> GetCourseWithDetailsAsync(int courseId); 
    }
}
