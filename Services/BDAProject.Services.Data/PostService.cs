namespace BDAProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Data.Common.Repositories;
    using BDAProject.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class PostService : IPostService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;

        public PostService(IDeletableEntityRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
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

        public async Task EditPost(string id, string text)
        {
            var post = await this.postRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            post.Text = text;
            post.CreatedOn = DateTime.UtcNow;
            this.postRepository.Update(post);

            await this.postRepository.SaveChangesAsync();
        }

        public async Task DeletePost(string postId)
        {
            var post = this.postRepository.All().FirstOrDefault(x => x.Id == postId);
            post.IsDeleted = true;
            post.DeletedOn = DateTime.Now;

            this.postRepository.Update(post);
            await this.postRepository.SaveChangesAsync();
        }

        public Post GetPost(string postId)
        {
            return this.postRepository.All().Where(p => p.Id == postId).FirstOrDefault();
        }

        public IEnumerable<Post> GetAll()
        {
            return this.postRepository.All()
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreatedOn).ToList();
        }
    }
}
