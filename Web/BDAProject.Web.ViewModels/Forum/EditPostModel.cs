namespace BDAProject.Web.ViewModels.Forum
{
    using Ganss.XSS;

    public class EditPostModel
    {
       public string Text { get; set; }

        public string SanitizedText
            => new HtmlSanitizer().Sanitize(this.Text);
    }
}
