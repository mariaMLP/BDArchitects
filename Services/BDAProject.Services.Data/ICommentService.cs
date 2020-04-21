namespace BDAProject.Services.Data
{
    using System.Threading.Tasks;

    using BDAProject.Data.Models;

    public interface ICommentService
    {
        Task CreateComment(string userId, string postId, string username, string commentText);

        Task EditComment(string id, string commentText);

        Task DeleteComment(string commentId);

        Comment GetComment(string commentId);
    }
}
