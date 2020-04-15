namespace BDAProject.Web.ViewModels.Forum
{
    using BDAProject.Data.Models;
    using BDAProject.Services.Mapping;
    using Ganss.XSS;

    public class EditCommentModel : IMapTo<Comment>
    {
        public string CommentText { get; set; }

        public string SanitizedText
            => new HtmlSanitizer().Sanitize(this.CommentText);
    }
}
