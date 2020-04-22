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

    public class BlogCommentServiceTests
    {
        [Fact]
        public async Task AddBlogCommentShoutAddItInDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var blogPostRepository = new EfDeletableEntityRepository<BlogPost>(dbContext);

            var blogCommentRepository = new EfDeletableEntityRepository<BlogComment>(dbContext);

            var service = new BlogCommentService(blogPostRepository, blogCommentRepository);

            dbContext.BlogPosts.Add(new BlogPost
            {
                Id = "blogPostId",
                ImageName = "imageName",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
            });

            await dbContext.SaveChangesAsync();

            await service.CreateBlogComment("userId", "blogPostId", "david", "commentText");

            Assert.Equal(1, dbContext.BlogComments.Count());
        }

        [Fact]
        public async Task EditBlogCommentShouldEditDataInDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var blogPostRepository = new EfDeletableEntityRepository<BlogPost>(dbContext);

            var blogCommentRepository = new EfDeletableEntityRepository<BlogComment>(dbContext);

            var service = new BlogCommentService(blogPostRepository, blogCommentRepository);

            dbContext.BlogPosts.Add(new BlogPost
            {
                Id = "blogPostId",
                ImageName = "imageName",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
            });

            await dbContext.SaveChangesAsync();

            dbContext.BlogComments.Add(new BlogComment
            {
                Id = "commentId",
                BlogPostId = "blogPostId",
                UserId = "userId",
                UserName = "username",
                CommentText = "commentText",
                CreatedOn = DateTime.UtcNow,
            });

            await dbContext.SaveChangesAsync();

            await service.EditBlogComment("commentId", "newCommentText");

            var blogComment = dbContext.BlogComments.FirstOrDefault(c => c.Id == "commentId");

            Assert.Equal("newCommentText", blogComment.CommentText);
        }

        [Fact]
        public async Task DeleteBlogCommentShouldTurnItToIsDeleted()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var blogPostRepository = new EfDeletableEntityRepository<BlogPost>(dbContext);

            var blogCommentRepository = new EfDeletableEntityRepository<BlogComment>(dbContext);

            var service = new BlogCommentService(blogPostRepository, blogCommentRepository);

            dbContext.BlogPosts.Add(new BlogPost
            {
                Id = "blogPostId",
                ImageName = "imageName",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
            });

            await dbContext.SaveChangesAsync();

            dbContext.BlogComments.Add(new BlogComment
            {
                Id = "commentId",
                BlogPostId = "blogPostId",
                UserId = "userId",
                UserName = "username",
                CommentText = "commentText",
                CreatedOn = DateTime.UtcNow,
            });

            await dbContext.SaveChangesAsync();

            await service.DeleteBlogComment("commentId");

            Assert.True(dbContext.BlogComments.FindAsync("commentId").Result.IsDeleted);
        }

        [Fact]
        public void GetBlogCommentShouldGetCommentByIdFromDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var blogPostRepository = new EfDeletableEntityRepository<BlogPost>(dbContext);

            var blogCommentRepository = new EfDeletableEntityRepository<BlogComment>(dbContext);

            var service = new BlogCommentService(blogPostRepository, blogCommentRepository);

            dbContext.BlogPosts.Add(new BlogPost
            {
                Id = "blogPostId",
                ImageName = "imageName",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
            });

            dbContext.SaveChanges();

            dbContext.BlogComments.Add(new BlogComment
            {
                Id = "commentId",
                BlogPostId = "blogPostId",
                UserId = "userId",
                UserName = "username",
                CommentText = "commentText",
                CreatedOn = DateTime.UtcNow,
            });

            dbContext.SaveChanges();

            var blogComment = service.GetBlogComment("commentId");

            Assert.Equal("username", blogComment.UserName);
            Assert.Equal("commentText", blogComment.CommentText);
        }
    }
}
