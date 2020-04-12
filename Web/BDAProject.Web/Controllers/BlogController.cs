namespace BDAProject.Web.Controllers
{
    using BDAProject.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class BlogController : Controller
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public IActionResult Blog()
        {
            return this.View(this.blogService.GetAll());
        }
    }
}
