namespace BDAProject.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Models;

    public interface IBlogService
    {
        Task CreateAsync(string userId, string text, string username);

        IQueryable<Post> GetAll();
    }
}
