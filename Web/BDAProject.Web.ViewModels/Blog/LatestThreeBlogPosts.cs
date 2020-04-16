namespace BDAProject.Web.ViewModels.Blog
{
    using System.Collections.Generic;

    public class LatestThreeBlogPosts
    {
        public IEnumerable<BlogPostAllModel> BlogPosts { get; set; }
    }
}
