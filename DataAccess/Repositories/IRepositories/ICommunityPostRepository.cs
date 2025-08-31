using Entities.Models;


namespace DataAccess.Repositories.IRepositories
{
    public interface ICommunityPostRepository : IRepository<CommunityPost> {
        Task<CommunityPost?> GetPostWithDetailsAsync(int postId);
        Task<List<CommunityPost>> GetPostsByCourseAsync(int courseId);
    }
}
