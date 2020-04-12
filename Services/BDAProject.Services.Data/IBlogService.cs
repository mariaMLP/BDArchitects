namespace BDAProject.Services.Data
{
    using System.Linq;

    using BDAProject.Data.Models;

    public interface IBlogService
    {
        IQueryable<BlogPost> GetAll();
    }
}
