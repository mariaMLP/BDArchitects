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
                .Execute(contactModel.SanitizedName, contactModel.SanitizedEmail, contactModel.SanitizedSubject, contactModel.SanitizedMessage)
                .Wait();

            return this.Redirect($"/");
        }
    }
}
