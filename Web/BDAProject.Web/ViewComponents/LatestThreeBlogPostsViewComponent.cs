namespace BDAProject.Web.ViewComponents
{
    using BDAProject.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "LatestThreeBlogPosts")]
    public class LatestThreeBlogPostsViewComponent : ViewComponent
    {
        private readonly IBlogPostService blogService;

        public LatestThreeBlogPostsViewComponent(IBlogPostService blogService)
        {
            this.blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            return this.View(this.blogService.GetLatestThreeBlogPosts());
        }
    }
}
