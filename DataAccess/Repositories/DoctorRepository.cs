using DataAccess.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        //public async Task<Doctor?> GetDoctorWithCoursesAsync(int id)
        //{
        //    return await _context.Doctors
        //        .Include(d => d.Courses)
        //        .FirstOrDefaultAsync(d => d.Id == id);
        //}
    }
}
