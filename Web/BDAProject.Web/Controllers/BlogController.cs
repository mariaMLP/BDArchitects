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

        public IActionResult Blog()
        {
            return this.View(this.blogService.GetAll());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Blog(string blogPostId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var username = this.userManager.GetUserName(this.User);

            await this.blogService.CreateBlogLike(userId, blogPostId, username);

            return this.Redirect($"/Blog/Blog");
        }

        [Authorize]
        public IActionResult AddBlogComment()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBlogComment(string blogPostId, BlogCommentAddInputModel blogCommentInput)
        {
            var userId = this.userManager.GetUserId(this.User);
            var username = this.userManager.GetUserName(this.User);

            await this.blogService.CreateBlogComment(userId, blogPostId, username, blogCommentInput.SanitizedCommentText);

            return this.Redirect($"/Blog/Blog");
        }

        [Authorize]
        public IActionResult EditBlogComment(string blogCommentId)
        {
            return this.View(this.blogService.GetBlogComment(blogCommentId));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditBlogComment(string blogCommentId, EditBlogCommentModel editBlogCommentModel)
        {
            await this.blogService.EditBlogComment(blogCommentId, editBlogCommentModel.SanitizedCommentText);

            return this.Redirect($"/Blog/Blog");
        }
    }
}
