namespace BDAProject.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Common.Repositories;
    using BDAProject.Data.Models;

    public class BlogLikeService : IBlogLikeService
    {
        private readonly IDeletableEntityRepository<BlogPost> blogPostRepository;
        private readonly IRepository<BlogLike> blogLikeRepository;

        public BlogLikeService(IDeletableEntityRepository<BlogPost> blogPostRepository, IRepository<BlogLike> blogLikeRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogLikeRepository = blogLikeRepository;
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
    }
}
