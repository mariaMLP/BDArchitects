namespace BDAProject.Services
{
    using System;
    using System.Threading.Tasks;

    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class ContactService : IContactService
    {
        public async Task Execute(string name, string email, string subjectt, string message)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress($"{email}", $"{name}");
            var subject = $"{subjectt}";
            var to = new EmailAddress("mariapanamska@gmail.com", "Example User");
            var plainTextContent = $"{message}";
            var htmlContent = $"<strong>{message}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
