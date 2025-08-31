using DataAccess.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class InstructorRepository : Repository<Instructor>, IInstructorRepository
    {
        private readonly ApplicationDbContext _context;
        public InstructorRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
