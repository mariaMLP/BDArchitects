namespace BDAProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Common.Repositories;
    using BDAProject.Data.Models;
    using BDAProject.Web.ViewModels.Blog;

    public class BlogService : IBlogService
    {
        private readonly IRepository<Post> repository;

        public BlogService(IRepository<Post> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(string userId, string text, string username)
        {
            var postId = Guid.NewGuid().ToString();

            await this.repository.AddAsync(new Post
            {
                Id = postId,
                Text = text,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                UserName = username,
            });

            await this.repository.SaveChangesAsync();
        }

        public IQueryable<Post> GetAll()
        {
            return this.repository.All();
        }
    }
}
