namespace BDAProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Models;

    public interface IBlogPostService
    {
        IQueryable<BlogPost> GetAll();

        Task DeleteBlogPost(string blogPostId);

        IEnumerable<BlogPost> GetLatestThreeBlogPosts();
    }
}
