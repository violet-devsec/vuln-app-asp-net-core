using Microsoft.AspNetCore.Mvc;

namespace PicShopper.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
