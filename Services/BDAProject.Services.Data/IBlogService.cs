namespace BDAProject.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Models;

    public interface IBlogService
    {
        Task CreatePost(string userId, string text, string username);

        Task CreateLike(string userId, string postId, string username);

        IQueryable<Post> GetAll();
    }
}
