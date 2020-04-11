namespace BDAProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BlogController : Controller
    {
        public IActionResult Blog()
        {
            return this.View();
        }
    }
}
