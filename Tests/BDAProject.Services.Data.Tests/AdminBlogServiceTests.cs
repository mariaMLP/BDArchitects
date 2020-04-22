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

    public class AdminBlogServiceTests
    {
        [Fact]
        public async Task AddAdminPostShoutAddItInDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var blogPostRepository = new EfDeletableEntityRepository<BlogPost>(dbContext);

            var service = new AdminBlogService(blogPostRepository);

            await service.CreateAdminPost("imageName", "postText");

            Assert.Equal(1, dbContext.BlogPosts.Count());
        }
    }
}
