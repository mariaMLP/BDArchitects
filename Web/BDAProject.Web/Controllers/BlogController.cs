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
        private readonly IBlogPostService blogPostService;
        private readonly IBlogCommentService blogCommentService;
        private readonly IBlogLikeService blogLikeService;
        private readonly UserManager<ApplicationUser> userManager;

        public BlogController(IBlogPostService blogPostService, IBlogCommentService blogCommentService, IBlogLikeService blogLikeService,  UserManager<ApplicationUser> userManager)
        {
            this.blogPostService = blogPostService;
            this.blogCommentService = blogCommentService;
            this.blogLikeService = blogLikeService;
            this.userManager = userManager;
        }

        public IActionResult Blog()
        {
            return this.View(this.blogPostService.GetAllPosts<BlogPostAllModel>());
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

            await this.blogCommentService.CreateBlogComment(userId, blogPostId, username, blogCommentInput.SanitizedCommentText);

            return this.Redirect($"/Blog/Blog");
        }

        [Authorize]
        public IActionResult EditBlogComment(string blogCommentId)
        {
            return this.View(this.blogCommentService.GetBlogComment(blogCommentId));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditBlogComment(string blogCommentId, EditBlogCommentModel editBlogCommentModel)
        {
            await this.blogCommentService.EditBlogComment(blogCommentId, editBlogCommentModel.SanitizedCommentText);

            return this.Redirect($"/Blog/Blog");
        }

        public IActionResult DeleteBlogPost()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteBlogPost(string blogPostId)
        {
            await this.blogPostService.DeleteBlogPost(blogPostId);

            return this.Redirect($"/Blog/Blog");
        }

        public IActionResult DeleteBlogComment()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteBlogComment(string blogCommentId)
        {
            await this.blogCommentService.DeleteBlogComment(blogCommentId);

            return this.Redirect($"/Blog/Blog");
        }
    }
}
