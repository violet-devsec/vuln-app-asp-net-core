using Microsoft.AspNetCore.Mvc;
using PicShopper.Web.Models;
using PicShopper.Web.Services;
using PicShopper.Web.ViewModels;

namespace PicShopper.Web.Controllers
{
    public class GuestBookController : Controller
    {
        private IGuestBookData _guestBook;

        public GuestBookController(IGuestBookData guestBookData)
        {
            _guestBook = guestBookData;
        }
        public IActionResult Index()
        {
            var model = new GuestBookViewModel();
            model.Comments = _guestBook.GetComments();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(GuestBook model)
        {
            var guestBook     = new GuestBook();
            guestBook.Name    = model.Name;
            guestBook.Comment = model.Comment;

            bool success = _guestBook.AddComment(guestBook);

            if (success)
            {
                return RedirectToAction("Index", "GuestBook");
            }
            else
            {
                return View();
            }
        }
    }
}
