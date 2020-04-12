namespace BDAProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using BDAProject.Data.Common.Models;

    public class BlogPost : BaseDeletableModel<string>
    {
        public BlogPost()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public ICollection<BlogLike> BlogLikes { get; set; } = new HashSet<BlogLike>();

        public ICollection<BlogComment> BlogComments { get; set; } = new HashSet<BlogComment>();
    }
}
