using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PicShopper.Web.Controllers
{
    [Authorize]
    public class RecentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
