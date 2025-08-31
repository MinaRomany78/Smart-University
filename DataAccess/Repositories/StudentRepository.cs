using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        //public async Task<Student?> GetByNationalIdAsync(string nationalId)
        //{
        //    return await _context.Students
        //        .Include(s => s.Enrollments)
        //        .FirstOrDefaultAsync(s => s.NationalID == nationalId);
        //}
    }
}
