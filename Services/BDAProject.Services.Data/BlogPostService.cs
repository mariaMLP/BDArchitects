namespace BDAProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Common.Repositories;
    using BDAProject.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BlogPostService : IBlogPostService
    {
        private readonly IDeletableEntityRepository<BlogPost> blogPostRepository;

        public BlogPostService(IDeletableEntityRepository<BlogPost> blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public IQueryable<BlogPost> GetAll()
        {
            return this.blogPostRepository.All().Include(p => p.BlogLikes)
                .Include(p => p.BlogComments)
                .OrderByDescending(p => p.CreatedOn);
        }

        public async Task DeleteBlogPost(string blogPostId)
        {
            var blogPost = this.blogPostRepository.All().FirstOrDefault(x => x.Id == blogPostId);
            blogPost.IsDeleted = true;
            blogPost.DeletedOn = DateTime.Now;

            this.blogPostRepository.Update(blogPost);
            await this.blogPostRepository.SaveChangesAsync();
        }

        public IEnumerable<BlogPost> GetLatestThreeBlogPosts()
        {
            return this.blogPostRepository.All().Include(p => p.BlogLikes)
                .Include(p => p.BlogComments)
                .OrderByDescending(p => p.CreatedOn).Take(3).ToList();
        }
    }
}
