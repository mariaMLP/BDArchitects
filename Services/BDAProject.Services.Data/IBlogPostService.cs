namespace BDAProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Models;

    public interface IBlogPostService
    {
        IEnumerable<BlogPost> GetAll();

        Task DeleteBlogPost(string blogPostId);

        IEnumerable<BlogPost> GetLatestThreeBlogPosts();

        IEnumerable<T> GetAllPosts<T>();
    }
}
