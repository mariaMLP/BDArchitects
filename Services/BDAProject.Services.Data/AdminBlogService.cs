namespace BDAProject.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Common.Repositories;
    using BDAProject.Data.Models;

    public class AdminBlogService : IAdminBlogService
    {
        private readonly IDeletableEntityRepository<BlogPost> blogPostRepository;

        public AdminBlogService(IDeletableEntityRepository<BlogPost> blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task CreateAdminPost(string imageName, string text)
        {
            var postId = Guid.NewGuid().ToString();

            await this.blogPostRepository.AddAsync(new BlogPost
            {
                Id = postId,
                ImageName = imageName,
                Text = text,
                CreatedOn = DateTime.UtcNow,
                UserId = "f1f76718-25dd-4c3f-863e-ab9c1c248c1d",
            });

            await this.blogPostRepository.SaveChangesAsync();
        }
    }
}
