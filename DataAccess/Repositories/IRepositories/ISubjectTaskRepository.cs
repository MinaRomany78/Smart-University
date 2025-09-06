using Entities.Models;


namespace DataAccess.Repositories.IRepositories
{
    public interface ISubjectTaskRepository : IRepository<SubjectTask>
    {
        Task<SubjectTask?> GetTaskWithSubmissionsAsync(int taskId);
    }

}
