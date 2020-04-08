namespace BDAProject.Web.Controllers
{
    using System.Threading.Tasks;

    using BDAProject.Data.Models;
    using BDAProject.Services.Data;
    using BDAProject.Web.ViewModels.Blog;
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

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostAddInputModel postInput)
        {
            var userId = this.userManager.GetUserId(this.User);
            var username = this.userManager.GetUserName(this.User);

            await this.blogService.CreateAsync(userId, postInput.Text, username);

            return this.Redirect($"/Blog/All");
        }
    }
}
