namespace BDAProject.Services.Data
{
    using System.Linq;

    using BDAProject.Data.Common.Repositories;
    using BDAProject.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BlogService : IBlogService
    {
        private readonly IDeletableEntityRepository<BlogPost> blogPostRepository;

        public BlogService(IDeletableEntityRepository<BlogPost> blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public IQueryable<BlogPost> GetAll()
        {
            return this.blogPostRepository.All().Include(p => p.BlogLikes)
                .Include(p => p.BlogComments)
                .OrderByDescending(p => p.CreatedOn);
        }
    }
}
