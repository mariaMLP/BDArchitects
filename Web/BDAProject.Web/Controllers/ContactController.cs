namespace BDAProject.Web.Controllers
{
    using BDAProject.Services;
    using BDAProject.Web.ViewModels.Contact;
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : Controller
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contact(ContactInputModel contactModel)
        {
            this.contactService
                .Execute(contactModel.Name, contactModel.Email, contactModel.Subject, contactModel.Message)
                .Wait();

            return this.Redirect($"/");
        }
    }
}
