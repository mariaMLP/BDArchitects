namespace BDAProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return this.View();
        }
    }
}
