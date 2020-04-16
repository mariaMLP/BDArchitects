namespace BDAProject.Web.ViewComponents
{
    using BDAProject.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "LatestThreeBlogPosts")]
    public class LatestThreeBlogPostsViewComponent : ViewComponent
    {
        private readonly IBlogService blogService;

        public LatestThreeBlogPostsViewComponent(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            return this.View(this.blogService.GetLatestThreeBlogPosts());
        }
    }
}
