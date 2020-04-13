namespace BDAProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using BDAProject.Data.Common.Models;

    public class BlogComment : BaseDeletableModel<string>
    {
        public BlogComment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string CommentText { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
