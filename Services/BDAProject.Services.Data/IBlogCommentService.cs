namespace BDAProject.Services.Data
{
    using System.Threading.Tasks;

    using BDAProject.Data.Models;

    public interface IBlogCommentService
    {
        Task CreateBlogComment(string userId, string blogPostId, string username, string commentText);

        Task EditBlogComment(string id, string commentText);

        Task DeleteBlogComment(string blogCommentId);

        BlogComment GetBlogComment(string blogPostId);
    }
}
