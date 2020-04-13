namespace BDAProject.Web.ViewModels.Forum
{
    using Ganss.XSS;

    public class EditCommentModel
    {
        public string Id { get; set; }

        public string CommentText { get; set; }

        public string SanitizedText
            => new HtmlSanitizer().Sanitize(this.CommentText);
    }
}
