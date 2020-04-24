namespace BDAProject.Services.Data
{
    using System.Threading.Tasks;

    public interface ILikeService
    {
        Task CreateLike(string userId, string postId, string username);

        int GetLikes(string postId);
    }
}
