namespace BDAProject.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Common.Repositories;
    using BDAProject.Data.Models;

    public class LikeService : ILikeService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly IRepository<Like> likeRepository;

        public LikeService(IDeletableEntityRepository<Post> postRepository, IRepository<Like> likeRepository)
        {
            this.postRepository = postRepository;
            this.likeRepository = likeRepository;
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

        public int GetLikes(string postId)
        {
            var likes = this.likeRepository.All()
                .Where(x => x.PostId == postId).Count();
            return likes;
        }
    }
}
