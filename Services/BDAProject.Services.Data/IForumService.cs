namespace BDAProject.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Models;

    public interface IForumService
    {
        Task CreatePost(string userId, string text, string username);

        Task CreateLike(string userId, string postId, string username);

        Task CreateComment(string userId, string postId, string username, string commentText);

        int GetAllLikesById(string postId);

        IQueryable<Post> GetAll();

        Post GetPost(string postId);

        Task EditPost(string id, string text);
    }
}
