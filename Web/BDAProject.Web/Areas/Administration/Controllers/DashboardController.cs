namespace BDAProject.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using BDAProject.Services.Data;
    using BDAProject.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
