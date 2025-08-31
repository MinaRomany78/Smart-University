using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostFileRepository : Repository<PostFile>, IPostFileRepository
    {
        private readonly ApplicationDbContext _context;
        public PostFileRepository(ApplicationDbContext context) : base(context) { 
            _context = context;
        }
  

    }
}
