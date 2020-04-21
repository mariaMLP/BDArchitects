namespace BDAProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Models;

    public interface IPostService
    {
        Task CreatePost(string userId, string text, string username);

        Task EditPost(string id, string text);

        Task DeletePost(string postId);

        Post GetPost(string postId);

        IEnumerable<Post> GetAll();
    }
}
