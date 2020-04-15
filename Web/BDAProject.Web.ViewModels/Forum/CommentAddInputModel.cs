namespace BDAProject.Web.ViewModels.Blog
{
    using BDAProject.Data.Models;
    using BDAProject.Services.Mapping;
    using Ganss.XSS;

    public class CommentAddInputModel : IMapTo<Comment>
    {
        public string CommentText { get; set; }

        public string SanitizedCommentText
            => new HtmlSanitizer().Sanitize(this.CommentText);

        public string PostId { get; set; }
    }
}
