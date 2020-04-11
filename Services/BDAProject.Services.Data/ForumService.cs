namespace BDAProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Common.Repositories;
    using BDAProject.Data.Models;
    using BDAProject.Web.ViewModels.Blog;
    using Microsoft.EntityFrameworkCore;

    public class ForumService : IForumService
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<Like> likeRepository;
        private readonly IRepository<Comment> commentRepository;

        public ForumService(IRepository<Post> postRepository, IRepository<Like> likeRepository, IRepository<Comment> commentRepository)
        {
            this.postRepository = postRepository;
            this.likeRepository = likeRepository;
            this.commentRepository = commentRepository;
        }

        public async Task CreatePost(string userId, string text, string username)
        {
            var postId = Guid.NewGuid().ToString();

            await this.postRepository.AddAsync(new Post
            {
                Id = postId,
                Text = text,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                UserName = username,
            });

            await this.postRepository.SaveChangesAsync();
        }

        public async Task CreateLike(string userId, string postId, string username)
        {
            var like = this.likeRepository.All()
                .Where(l => l.PostId == postId && l.UserId == userId)
                .FirstOrDefault();

            if (like == null)
            {
                var likeId = Guid.NewGuid().ToString();

                var likeNew = new Like
                {
                    Id = likeId,
                    PostId = postId,
                    UserId = userId,
                    UserName = username,
                };

                await this.likeRepository.AddAsync(likeNew);

                await this.likeRepository.SaveChangesAsync();

                var post = this.postRepository.All()
                .Where(p => p.Id == postId)
                .FirstOrDefault();

                post.Likes.Add(likeNew);
            }
        }

        public async Task CreateComment(string userId, string postId, string username, string commentText)
        {
            var commentId = Guid.NewGuid().ToString();

            var comment = new Comment
            {
                Id = commentId,
                PostId = postId,
                UserId = userId,
                UserName = username,
                CommentText = commentText,
                CreatedOn = DateTime.UtcNow,
            };

            await this.commentRepository.AddAsync(comment);

            await this.commentRepository.SaveChangesAsync();

            var post = this.postRepository.All()
            .Where(p => p.Id == postId)
            .FirstOrDefault();

            post.Comments.Add(comment);
        }

        public int GetAllLikesById(string postId)
        {
            return this.likeRepository.All().Where(l => l.PostId == postId).Count();
        }

        public IQueryable<Post> GetAll()
        {
            return this.postRepository.All().Include(p => p.Likes)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreatedOn);
        }

        public Post GetPost(string postId)
        {
            return this.postRepository.All().Where(p => p.Id == postId).FirstOrDefault();
        }
    }
}
