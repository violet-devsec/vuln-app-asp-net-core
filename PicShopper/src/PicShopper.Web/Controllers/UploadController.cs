using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicShopper.Web.Models;
using PicShopper.Web.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading;
//using System.Security.Claims;

namespace PicShopper.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class UploadController : Controller
    {
        private IUploads _uploads { get; set; }
        public UploadController(IUploads uploads)
        {
            _uploads = uploads;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UploadFile model)
        {
            
            var userId = User.Identities.First().Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault();

            bool success = _uploads.Upload(model.Content, model.Title, model.Price, userId).Result;

            if(success)
            {
                return RedirectToAction("Index", "Recent");
            }
            else
            {
                return View();
            }
        }
    }
}
