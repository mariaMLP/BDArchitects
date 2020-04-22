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

    public class LikeServiceTests
    {
        [Fact]
        public async Task AddLikeShoutAddItInDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var postRepository = new EfDeletableEntityRepository<Post>(dbContext);

            var likeRepository = new EfRepository<Like>(dbContext);

            dbContext.Posts.Add(new Post
            {
                Id = "postId",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
                UserName = "username",
            });

            await dbContext.SaveChangesAsync();

            var service = new LikeService(postRepository, likeRepository);

            await service.CreateLike("userId", "postId", "david");

            Assert.Equal(1, dbContext.Likes.Count());
        }
    }
}
