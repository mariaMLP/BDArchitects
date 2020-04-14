namespace BDAProject.Web.ViewModels.Blog
{
    using Ganss.XSS;

    public class EditBlogCommentModel
    {
        public string CommentText { get; set; }

        public string SanitizedCommentText
           => new HtmlSanitizer().Sanitize(this.CommentText);
    }
}
