namespace BDAProject.Web.ViewModels.Forum
{
    using BDAProject.Data.Models;
    using BDAProject.Services.Mapping;
    using Ganss.XSS;

    public class EditPostModel : IMapTo<Post>
    {
       public string Text { get; set; }

       public string SanitizedText
            => new HtmlSanitizer().Sanitize(this.Text);
    }
}
