namespace BDAProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public string UserName { get; set; }
    }
}
