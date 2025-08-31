using DataAccess.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class AssistantRepository : Repository<Assistant>, IAssistantRepository
    {
        private readonly ApplicationDbContext _context;
        public AssistantRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        //public async Task<Assistant?> GetAssistantWithCoursesAsync(int id)
        //{
        //    return await _context.Assistants
        //        .Include(a => a.Course)
        //        .FirstOrDefaultAsync(a => a.Id == id);
        //}
    }
}
