namespace BDAProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ProjectController : Controller
    {
        public IActionResult Project()
        {
            return this.View();
        }
    }
}
