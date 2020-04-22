namespace BDAProject.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using BDAProject.Data;
    using BDAProject.Data.Models;
    using BDAProject.Data.Repositories;
    using BDAProject.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PostServiceTests
    {
        [Fact]
        public async Task AddPostShoutAddItInDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var postRepository = new EfDeletableEntityRepository<Post>(dbContext);

            var service = new PostService(postRepository);

            await service.CreatePost("userIdText", "postText", "david");

            Assert.Equal(1, dbContext.Posts.Count());
        }

        [Fact]
        public async Task EditPostShouldEditDataInDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var postRepository = new EfDeletableEntityRepository<Post>(dbContext);

            var service = new PostService(postRepository);

            dbContext.Posts.Add(new Post
            {
                Id = "postId",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
                UserName = "username",
            });

            await dbContext.SaveChangesAsync();

            await service.EditPost("postId", "newPostText");

            var post = dbContext.Posts.FirstOrDefault(p => p.Id == "postId");

            Assert.Equal("newPostText", post.Text);
        }

        [Fact]
        public async Task DeletePostShouldTurnItToIsDeleted()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var postRepository = new EfDeletableEntityRepository<Post>(dbContext);

            var service = new PostService(postRepository);

            await postRepository.AddAsync(new Post
            {
                Id = "postId",
                Text = "text",
                CreatedOn = DateTime.Now,
                UserId = "userId",
                UserName = "username",
            });

            await postRepository.SaveChangesAsync();

            await service.DeletePost("postId");

            Assert.True(dbContext.Posts.FindAsync("postId").Result.IsDeleted);
        }

        [Fact]
        public void GetPostShouldGetPostByIdFromDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var postRepository = new EfDeletableEntityRepository<Post>(dbContext);

            var service = new PostService(postRepository);

            dbContext.Posts.Add(new Post
            {
                Id = "postId",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
                UserName = "username",
            });

            dbContext.SaveChanges();

            var post = service.GetPost("postId");

            Assert.Equal("username", post.UserName);
        }

        [Fact]
        private void GetAllShouldReturnListOfPostsFromDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var postRepository = new EfDeletableEntityRepository<Post>(dbContext);

            var service = new PostService(postRepository);

            dbContext.Posts.Add(new Post
            {
                Id = "postId",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
                UserName = "username",
            });

            dbContext.SaveChanges();

            var posts = service.GetAll();

            Assert.True(posts.Count() == 1);
        }
    }
}
