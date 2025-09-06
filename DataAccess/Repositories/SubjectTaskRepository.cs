using DataAccess.Data;
using DataAccess.Repositories.IRepositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class SubjectTaskRepository : Repository<SubjectTask>, ISubjectTaskRepository
    {
        private readonly ApplicationDbContext _context;
        public SubjectTaskRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
        public async Task<SubjectTask?> GetTaskWithSubmissionsAsync(int taskId)
        {
            return await _context.SubjectTasks
                .Include(t => t.TaskSubmissions)
                    .ThenInclude(s => s.Student)
                        .ThenInclude(st => st.ApplicationUser)
                .Include(t => t.UniversityCourse)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }
    }
}
