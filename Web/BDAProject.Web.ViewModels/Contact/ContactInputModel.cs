namespace BDAProject.Web.ViewModels.Contact
{
    using Ganss.XSS;

    public class ContactInputModel
    {
        public string Name { get; set; }

        public string SanitizedName
           => new HtmlSanitizer().Sanitize(this.Name);

        public string Email { get; set; }

        public string SanitizedEmail
          => new HtmlSanitizer().Sanitize(this.Email);

        public string Subject { get; set; }

        public string SanitizedSubject
          => new HtmlSanitizer().Sanitize(this.Subject);

        public string Message { get; set; }

        public string SanitizedMessage
          => new HtmlSanitizer().Sanitize(this.Message);
    }
}
