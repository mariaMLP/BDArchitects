namespace BDAProject.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Common.Repositories;
    using BDAProject.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentService(IDeletableEntityRepository<Post> postRepository, IDeletableEntityRepository<Comment> commentRepository)
        {
            this.postRepository = postRepository;
            this.commentRepository = commentRepository;
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

        public async Task EditComment(string id, string commentText)
        {
            var comment = await this.commentRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            comment.CommentText = commentText;
            comment.CreatedOn = DateTime.UtcNow;
            this.commentRepository.Update(comment);

            await this.commentRepository.SaveChangesAsync();
        }

        public Comment GetComment(string commentId)
        {
            return this.commentRepository.All().Where(c => c.Id == commentId).FirstOrDefault();
        }

        public async Task DeleteComment(string commentId)
        {
            var comment = this.commentRepository.All().FirstOrDefault(x => x.Id == commentId);
            comment.IsDeleted = true;
            comment.DeletedOn = DateTime.Now;

            this.commentRepository.Update(comment);
            await this.commentRepository.SaveChangesAsync();
        }
    }
}
