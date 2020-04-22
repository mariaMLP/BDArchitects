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

    public class BlogPostServiceTests
    {
        [Fact]
        public async Task DeleteBlogPostShouldTurnItToIsDeleted()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var blogPostRepository = new EfDeletableEntityRepository<BlogPost>(dbContext);

            var service = new BlogPostService(blogPostRepository);

            await blogPostRepository.AddAsync(new BlogPost
            {
                Id = "blogPostId",
                ImageName = "imageName",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
            });

            await blogPostRepository.SaveChangesAsync();

            await service.DeleteBlogPost("blogPostId");

            Assert.True(dbContext.BlogPosts.FindAsync("blogPostId").Result.IsDeleted);
        }

        [Fact]
        private void GetAllShouldReturnListOfBlogPostsFromDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var blogPostRepository = new EfDeletableEntityRepository<BlogPost>(dbContext);

            var service = new BlogPostService(blogPostRepository);

            dbContext.BlogPosts.Add(new BlogPost
            {
                Id = "blogPostId",
                ImageName = "imageName",
                Text = "text",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId",
            });

            dbContext.SaveChanges();

            var blogPosts = service.GetAll();

            Assert.True(blogPosts.Count() == 1);
        }

        [Fact]
        private void GetLatestThreeBlogPostsShouldReturnListOf3BlogPostsFromDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var blogPostRepository = new EfDeletableEntityRepository<BlogPost>(dbContext);

            var service = new BlogPostService(blogPostRepository);

            dbContext.BlogPosts.Add(new BlogPost
            {
                Id = "blogPostId1",
                ImageName = "imageName1",
                Text = "text1",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId1",
            });

            dbContext.BlogPosts.Add(new BlogPost
            {
                Id = "blogPostId2",
                ImageName = "imageName2",
                Text = "text2",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId2",
            });

            dbContext.BlogPosts.Add(new BlogPost
            {
                Id = "blogPostId3",
                ImageName = "imageName3",
                Text = "text3",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId3",
            });

            dbContext.BlogPosts.Add(new BlogPost
            {
                Id = "blogPostId4",
                ImageName = "imageName4",
                Text = "text4",
                CreatedOn = DateTime.UtcNow,
                UserId = "userId4",
            });

            dbContext.SaveChanges();

            var blogPostsLatest3 = service.GetLatestThreeBlogPosts();

            var blogPost = blogPostsLatest3.FirstOrDefault(b => b.Id == "blogPostId1");

            Assert.True(blogPostsLatest3.Count() == 3);
            Assert.True(blogPost == null);
        }
    }
}
