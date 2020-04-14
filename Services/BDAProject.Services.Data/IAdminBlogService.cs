namespace BDAProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IAdminBlogService
    {
        Task CreateAdminPost(string imageName, string text);

        Task DeleteBlogPost(string blogPostId);
    }
}
