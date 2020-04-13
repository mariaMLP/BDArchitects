namespace BDAProject.Web.ViewModels.Blog
{
    using Ganss.XSS;

    public class PostAddInputModel
    {
        public string UserId { get; set; }

        public string Text { get; set; }

        public string SanitizedText
            => new HtmlSanitizer().Sanitize(this.Text);
    }
}
