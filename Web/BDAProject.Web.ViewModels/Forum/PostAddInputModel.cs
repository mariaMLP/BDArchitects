namespace BDAProject.Web.ViewModels.Forum
{
    using BDAProject.Data.Models;
    using BDAProject.Services.Mapping;
    using Ganss.XSS;

    public class PostAddInputModel : IMapTo<Post>
    {
        public string UserId { get; set; }

        public string Text { get; set; }

        public string SanitizedText
            => new HtmlSanitizer().Sanitize(this.Text);
    }
}
