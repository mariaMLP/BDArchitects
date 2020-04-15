namespace BDAProject.Web.ViewModels.Administration.BlogAdmin
{
    using BDAProject.Data.Models;
    using BDAProject.Services.Mapping;
    using Ganss.XSS;

    public class AdminPostInputModel : IMapTo<BlogPost>
    {
        public string ImageName { get; set; }

        public string SanitizedImageName
           => new HtmlSanitizer().Sanitize(this.ImageName);

        public string Text { get; set; }

        public string SanitizedText
           => new HtmlSanitizer().Sanitize(this.Text);
    }
}
