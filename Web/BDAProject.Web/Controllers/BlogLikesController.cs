namespace BDAProject.Web.Controllers
{
    using System.Threading.Tasks;

    using BDAProject.Data.Models;
    using BDAProject.Services.Data;
    using BDAProject.Web.ViewModels.Likes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class BlogLikesController : ControllerBase
    {
        private readonly IBlogLikeService likeService;
        private readonly UserManager<ApplicationUser> userManager;

        public BlogLikesController(IBlogLikeService likeService, UserManager<ApplicationUser> userManager)
        {
            this.likeService = likeService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BlogLikeResponseModel>> MakeLike(BlogLikeViewModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            var username = this.userManager.GetUserName(this.User);

            await this.likeService.CreateBlogLike(userId, model.BlogPostId, username);

            var blogLikesCount = this.likeService.GetBlogLikes(model.BlogPostId);
            return new BlogLikeResponseModel { BlogLikesCount = blogLikesCount };
        }
    }
}
