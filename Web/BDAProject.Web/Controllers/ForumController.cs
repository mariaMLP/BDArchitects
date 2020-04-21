namespace BDAProject.Web.Controllers
{
    using System.Threading.Tasks;

    using BDAProject.Data.Models;
    using BDAProject.Services.Data;
    using BDAProject.Web.ViewModels.Blog;
    using BDAProject.Web.ViewModels.Forum;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;

    using Microsoft.AspNetCore.Mvc;

    public class ForumController : Controller
    {
        private readonly IPostService postService;
        private readonly ILikeService likeService;
        private readonly ICommentService commentService;
        private readonly UserManager<ApplicationUser> userManager;

        public ForumController(IPostService postService, ILikeService likeService, ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            this.postService = postService;
            this.likeService = likeService;
            this.commentService = commentService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            return this.View(this.postService.GetAll());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> All(string postId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var username = this.userManager.GetUserName(this.User);

            await this.likeService.CreateLike(userId, postId, username);

            return this.Redirect($"/Forum/All");
        }

        [Authorize]
        public IActionResult AddPost()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPost(PostAddInputModel postInput)
        {
            var userId = this.userManager.GetUserId(this.User);
            var username = this.userManager.GetUserName(this.User);

            await this.postService.CreatePost(userId, postInput.SanitizedText, username);

            return this.Redirect($"/Forum/All");
        }

        [Authorize]
        public IActionResult AddComment()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(CommentAddInputModel commentInput, string postId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var username = this.userManager.GetUserName(this.User);

            await this.commentService.CreateComment(userId, postId, username, commentInput.SanitizedCommentText);

            return this.Redirect($"/Forum/All");
        }

        [Authorize]
        public IActionResult EditPost(string postId)
        {
            return this.View(this.postService.GetPost(postId));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditPost(string postId, EditPostModel editPostModel)
        {
            await this.postService.EditPost(postId, editPostModel.SanitizedText);

            return this.Redirect($"/Forum/All");
        }

        [Authorize]
        public IActionResult EditComment(string commentId)
        {
            return this.View(this.commentService.GetComment(commentId));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditComment(string commentId, EditCommentModel editCommentModel)
        {
            await this.commentService.EditComment(commentId, editCommentModel.SanitizedText);

            return this.Redirect($"/Forum/All");
        }

        public IActionResult DeletePost()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeletePost(string postId)
        {
            await this.postService.DeletePost(postId);

            return this.Redirect($"/Forum/All");
        }

        public IActionResult DeleteComment()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteComment(string commentId)
        {
            await this.commentService.DeleteComment(commentId);

            return this.Redirect($"/Forum/All");
        }
    }
}
