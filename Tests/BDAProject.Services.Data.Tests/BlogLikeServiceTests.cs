namespace BDAProject.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data;
    using BDAProject.Data.Models;
    using BDAProject.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class BlogLikeServiceTests
    {
        [Fact]
        public async Task AddBlogLikeShoutAddItInDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var blogPostRepository = new EfDeletableEntityRepository<BlogPost>(dbContext);

            var blogLikeRepository = new EfRepository<BlogLike>(dbContext);

            dbContext.BlogPosts.Add(new BlogPost
            {
                Id = "blogPostId",
                ImageName = "imageName",
                Text = "text1",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
            });

            await dbContext.SaveChangesAsync();

            var service = new BlogLikeService(blogPostRepository, blogLikeRepository);

            await service.CreateBlogLike("userId", "blogPostId", "david");

            Assert.Equal(1, dbContext.BlogLikes.Count());
        }
    }
}
