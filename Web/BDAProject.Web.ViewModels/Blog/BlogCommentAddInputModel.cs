namespace BDAProject.Web.ViewModels.Blog
{
    using Ganss.XSS;

    public class BlogCommentAddInputModel
    {
        public string CommentText { get; set; }

        public string SanitizedCommentText
           => new HtmlSanitizer().Sanitize(this.CommentText);

        public string BlogPostId { get; set; }
    }
}
