namespace BDAProject.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using BDAProject.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class BlogAdminController : AdministrationController
    {
        public IActionResult AddAdminPost()
        {
            return this.View();
        }
    }
}
