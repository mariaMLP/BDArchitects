namespace BDAProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Like
    {
        public Like()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
