using DataAccess.Data;
using Entities.Models;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostLinkRepository: Repository<PostLink>, IPostLinkRepository
    {
        private readonly ApplicationDbContext _context;
        public PostLinkRepository (ApplicationDbContext context) : base(context) { 
            _context = context;
        }
       


    }
}
