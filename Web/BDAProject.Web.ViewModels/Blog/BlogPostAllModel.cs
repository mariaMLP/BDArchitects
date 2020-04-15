namespace BDAProject.Web.ViewModels.Blog
{
    using System.Collections.Generic;

    using BDAProject.Data.Models;
    using BDAProject.Services.Mapping;

    public class BlogPostAllModel : IMapFrom<BlogPost>
    {
        public string Id { get; set; }

        public string ImageName { get; set; }

        public string Text { get; set; }

        public string CreatedOn { get; set; }

        public ICollection<BlogLike> BlogLikes { get; set; }

        public ICollection<BlogComment> BlogComments { get; set; }
    }
}
