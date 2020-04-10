namespace BDAProject.Web.Controllers
{
    using System.Threading.Tasks;

    using BDAProject.Data.Models;
    using BDAProject.Services.Data;
    using BDAProject.Web.ViewModels.Blog;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;

    using Microsoft.AspNetCore.Mvc;

    public class BlogController : Controller
    {
        private readonly IBlogService blogService;
        private readonly UserManager<ApplicationUser> userManager;

        public BlogController(IBlogService blogService, UserManager<ApplicationUser> userManager)
        {
            this.blogService = blogService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            return this.View(this.blogService.GetAll());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> All(string postId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var username = this.userManager.GetUserName(this.User);

            await this.blogService.CreateLike(userId, postId, username);

            return this.Redirect($"/Blog/All");
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

            await this.blogService.CreatePost(userId, postInput.Text, username);

            return this.Redirect($"/Blog/All");
        }

        [Authorize]
        public IActionResult AddComment(string postId)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(CommentAddInputModel commentInput, string postId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var username = this.userManager.GetUserName(this.User);

            await this.blogService.CreateComment(userId, postId, username, commentInput.CommentText);

            return this.Redirect($"/Blog/All");
        }
    }
}
