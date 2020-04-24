namespace BDAProject.Web.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using BDAProject.Data.Models;
    using BDAProject.Services.Mapping;

    public class BlogPostAllModel : IMapFrom<BlogPost>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ImageName { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<BlogLike> BlogLikes { get; set; }

        public ICollection<BlogComment> BlogComments { get; set; }

        public int BlogLikesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BlogPost, BlogPostAllModel>()
                 .ForMember(x => x.BlogLikesCount, options =>
                 {
                     options.MapFrom(p => p.BlogLikes.Count());
                 });
        }
    }
}
