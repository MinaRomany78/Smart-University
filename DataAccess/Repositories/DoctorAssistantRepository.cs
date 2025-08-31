using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class DoctorAssistantRepository : Repository<DoctorAssistant>, IDoctorAssistantRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorAssistantRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

    }
}
