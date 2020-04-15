namespace BDAProject.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using BDAProject.Services.Data;
    using BDAProject.Web.ViewModels.Administration.BlogAdmin;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BlogAdminController : AdministrationController
    {
        private readonly IAdminBlogService adminBlogService;

        public BlogAdminController(IAdminBlogService adminBlogService)
        {
            this.adminBlogService = adminBlogService;
        }

        public IActionResult AddAdminPost()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminPost(AdminPostInputModel adminPost)
        {
            await this.adminBlogService.CreateAdminPost(adminPost.SanitizedImageName, adminPost.SanitizedText);

            return this.Redirect($"/Blog/Blog");
        }
    }
}
