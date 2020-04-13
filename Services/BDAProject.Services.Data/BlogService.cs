namespace BDAProject.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Common.Repositories;
    using BDAProject.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BlogService : IBlogService
    {
        private readonly IDeletableEntityRepository<BlogPost> blogPostRepository;
        private readonly IRepository<BlogLike> blogLikeRepository;
        private readonly IDeletableEntityRepository<BlogComment> blogCommentRepository;

        public BlogService(IDeletableEntityRepository<BlogPost> blogPostRepository, IRepository<BlogLike> blogLikeRepository, IDeletableEntityRepository<BlogComment> blogCommentRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogLikeRepository = blogLikeRepository;
            this.blogCommentRepository = blogCommentRepository;
        }

        public IQueryable<BlogPost> GetAll()
        {
            return this.blogPostRepository.All().Include(p => p.BlogLikes)
                .Include(p => p.BlogComments)
                .OrderByDescending(p => p.CreatedOn);
        }

        public async Task CreateBlogLike(string userId, string blogPostId, string username)
        {
            var blogLike = this.blogLikeRepository.All()
                .Where(l => l.BlogPostId == blogPostId && l.UserId == userId)
                .FirstOrDefault();

            if (blogLike == null)
            {
                var likeId = Guid.NewGuid().ToString();

                var blogLikeNew = new BlogLike
                {
                    Id = likeId,
                    BlogPostId = blogPostId,
                    UserId = userId,
                    UserName = username,
                };

                await this.blogLikeRepository.AddAsync(blogLikeNew);

                await this.blogLikeRepository.SaveChangesAsync();

                var post = this.blogPostRepository.All()
                .Where(p => p.Id == blogPostId)
                .FirstOrDefault();

                post.BlogLikes.Add(blogLikeNew);
            }
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

        public BlogComment GetBlogComment(string blogPostId)
        {
            return this.blogCommentRepository.All().Where(p => p.Id == blogPostId).FirstOrDefault();
        }
    }
}
