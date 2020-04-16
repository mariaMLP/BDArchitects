namespace BDAProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Models;

    public interface IForumService
    {
        Task CreatePost(string userId, string text, string username);

        Task CreateLike(string userId, string postId, string username);

        Task CreateComment(string userId, string postId, string username, string commentText);

        int GetAllLikesById(string postId);

        IEnumerable<Post> GetAll();

        Post GetPost(string postId);

        Task EditPost(string id, string text);

        Task EditComment(string id, string commentText);

        Comment GetComment(string commentId);

        Task DeletePost(string postId);

        Task DeleteComment(string commentId);
    }
}
