namespace BDAProject.Web.ViewModels.Blog
{
    using BDAProject.Data.Models;
    using BDAProject.Services.Mapping;
    using Ganss.XSS;

    public class BlogCommentAddInputModel : IMapTo<BlogComment>
    {
        public string CommentText { get; set; }

        public string SanitizedCommentText
           => new HtmlSanitizer().Sanitize(this.CommentText);
    }
}
