namespace BDAProject.Web.ViewModels.Forum
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using BDAProject.Data.Models;
    using BDAProject.Services.Mapping;

    public class PostAllModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Text { get; set; }

        public string UserName { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int LikesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostAllModel>()
                 .ForMember(x => x.LikesCount, options =>
                 {
                     options.MapFrom(p => p.Likes.Count());
                 });
        }
    }
}
