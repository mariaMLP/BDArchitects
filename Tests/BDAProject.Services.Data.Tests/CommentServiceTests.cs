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

    public class CommentServiceTests
    {
        [Fact]
        public async Task AddCommentShoutAddItInDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var postRepository = new EfDeletableEntityRepository<Post>(dbContext);

            var commentRepository = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new CommentService(postRepository, commentRepository);

            dbContext.Posts.Add(new Post
            {
                Id = "postId",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
                UserName = "username",
            });

            await dbContext.SaveChangesAsync();

            await service.CreateComment("userId", "postId", "david", "commentText");

            Assert.Equal(1, dbContext.Comments.Count());
        }

        [Fact]
        public async Task EditCommentShouldEditDataInDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var postRepository = new EfDeletableEntityRepository<Post>(dbContext);

            var commentRepository = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new CommentService(postRepository, commentRepository);

            dbContext.Posts.Add(new Post
            {
                Id = "postId",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
                UserName = "username",
            });

            await dbContext.SaveChangesAsync();

            dbContext.Comments.Add(new Comment
            {
                Id = "commentId",
                PostId = "postId",
                UserId = "userId",
                UserName = "username",
                CommentText = "commentText",
                CreatedOn = DateTime.UtcNow,
            });

            await dbContext.SaveChangesAsync();

            await service.EditComment("commentId", "newCommentText");

            var comment = dbContext.Comments.FirstOrDefault(c => c.Id == "commentId");

            Assert.Equal("newCommentText", comment.CommentText);
        }

        [Fact]
        public async Task DeleteCommentShouldTurnItToIsDeleted()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var postRepository = new EfDeletableEntityRepository<Post>(dbContext);

            var commentRepository = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new CommentService(postRepository, commentRepository);

            dbContext.Posts.Add(new Post
            {
                Id = "postId",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
                UserName = "username",
            });

            await dbContext.SaveChangesAsync();

            dbContext.Comments.Add(new Comment
            {
                Id = "commentId",
                PostId = "postId",
                UserId = "userId",
                UserName = "username",
                CommentText = "commentText",
                CreatedOn = DateTime.UtcNow,
            });

            await dbContext.SaveChangesAsync();

            await service.DeleteComment("commentId");

            Assert.True(dbContext.Comments.FindAsync("commentId").Result.IsDeleted);
        }

        [Fact]
        public void GetCommentShouldGetCommentByIdFromDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var postRepository = new EfDeletableEntityRepository<Post>(dbContext);

            var commentRepository = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new CommentService(postRepository, commentRepository);

            dbContext.Posts.Add(new Post
            {
                Id = "postId",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
                UserName = "username",
            });

            dbContext.SaveChanges();

            dbContext.Comments.Add(new Comment
            {
                Id = "commentId",
                PostId = "postId",
                UserId = "userId",
                UserName = "username",
                CommentText = "commentText",
                CreatedOn = DateTime.UtcNow,
            });

            dbContext.SaveChanges();

            var comment = service.GetComment("commentId");

            Assert.Equal("username", comment.UserName);
            Assert.Equal("commentText", comment.CommentText);
        }
    }
}
