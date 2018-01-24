using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicShopper.Web.Services;
using PicShopper.Web.ViewModels;

namespace PicShopper.Web.Controllers
{
    [Authorize]
    public class RecentController : Controller
    {
        private IRecent _recent { get; set; }
        public RecentController(IRecent recent)
        {
            _recent = recent;
        }
        public IActionResult Index()
        {
            var model         = new RecentViewModel();
            model.RecentFiles = _recent.GetRecentFiles();

            return View(model);
        }
    }
}
