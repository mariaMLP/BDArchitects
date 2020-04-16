namespace BDAProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Models;

    public interface IBlogService
    {
        IQueryable<BlogPost> GetAll();

        Task CreateBlogLike(string userId, string blogPostId, string username);

        Task CreateBlogComment(string userId, string blogPostId, string username, string commentText);

        Task EditBlogComment(string id, string commentText);

        BlogComment GetBlogComment(string blogPostId);

        Task DeleteBlogPost(string blogPostId);

        Task DeleteBlogComment(string blogCommentId);

        public IEnumerable<BlogPost> GetLatestThreeBlogPosts();
    }
}
