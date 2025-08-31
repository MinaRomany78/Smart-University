using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public class SupportTicketRepository : Repository<SupportTicket>, ISupportTicketRepository
    {
        private readonly ApplicationDbContext _context;
        public SupportTicketRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
    }
}
