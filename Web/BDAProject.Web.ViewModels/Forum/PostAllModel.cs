namespace BDAProject.Web.ViewModels.Blog
{
    using System.Collections.Generic;

    using BDAProject.Data.Models;
    using BDAProject.Services.Mapping;

    public class PostAllModel : IMapFrom<Post>
    {
        public string Id { get; set; }

        public string CreatedOn { get; set; }

        public string Text { get; set; }

        public string UserName { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
