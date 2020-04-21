namespace BDAProject.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Common.Repositories;
    using BDAProject.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BlogCommentService : IBlogCommentService
    {
        private readonly IDeletableEntityRepository<BlogPost> blogPostRepository;
        private readonly IDeletableEntityRepository<BlogComment> blogCommentRepository;

        public BlogCommentService(IDeletableEntityRepository<BlogPost> blogPostRepository, IDeletableEntityRepository<BlogComment> blogCommentRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogCommentRepository = blogCommentRepository;
        }

        public async Task CreateBlogComment(string userId, string blogPostId, string username, string commentText)
        {
            var blogCommentId = Guid.NewGuid().ToString();

            var blogComment = new BlogComment
            {
                Id = blogCommentId,
                BlogPostId = blogPostId,
                UserId = userId,
                UserName = username,
                CommentText = commentText,
                CreatedOn = DateTime.UtcNow,
            };

            await this.blogCommentRepository.AddAsync(blogComment);

            await this.blogCommentRepository.SaveChangesAsync();

            var post = this.blogPostRepository.All()
            .Where(p => p.Id == blogPostId)
            .FirstOrDefault();

            post.BlogComments.Add(blogComment);
        }

        public async Task EditBlogComment(string id, string commentText)
        {
            var blogComment = await this.blogCommentRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            blogComment.CommentText = commentText;
            blogComment.CreatedOn = DateTime.UtcNow;
            this.blogCommentRepository.Update(blogComment);

            await this.blogCommentRepository.SaveChangesAsync();
        }

        public async Task DeleteBlogComment(string blogCommentId)
        {
            var blogComment = this.blogCommentRepository.All().FirstOrDefault(x => x.Id == blogCommentId);
            blogComment.IsDeleted = true;
            blogComment.DeletedOn = DateTime.Now;

            this.blogCommentRepository.Update(blogComment);
            await this.blogCommentRepository.SaveChangesAsync();
        }

        public BlogComment GetBlogComment(string blogPostId)
        {
            return this.blogCommentRepository.All().Where(p => p.Id == blogPostId).FirstOrDefault();
        }
    }
}
