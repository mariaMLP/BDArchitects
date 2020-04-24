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
    public class LikesController : ControllerBase
    {
        private readonly ILikeService likeService;
        private readonly UserManager<ApplicationUser> userManager;

        public LikesController(ILikeService likeService, UserManager<ApplicationUser> userManager)
        {
            this.likeService = likeService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LikeResponseModel>> MakeLike(LikeViewModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            var username = this.userManager.GetUserName(this.User);

            await this.likeService.CreateLike(userId, model.PostId, username);

            var likesCount = this.likeService.GetLikes(model.PostId);
            return new LikeResponseModel { LikesCount = likesCount };
        }
    }
}
