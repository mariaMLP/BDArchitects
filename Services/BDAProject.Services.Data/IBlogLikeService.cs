namespace BDAProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IBlogLikeService
    {
        Task CreateBlogLike(string userId, string blogPostId, string username);

        int GetBlogLikes(string blogPostId);
    }
}
