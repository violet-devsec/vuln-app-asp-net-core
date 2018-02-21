using Microsoft.AspNetCore.Mvc;
using PicShopper.Web.Services;
using PicShopper.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicShopper.Web.Controllers
{
    public class CartController : Controller
    {
        private ICartData _cartData;
        public CartController(ICartData cartData)
        {
            _cartData = cartData;
        }
        public IActionResult Index(int id)
        {
            var model = new CartViewModel();
            model.CartItems = _cartData.GetCart(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart()
        {

            return RedirectToAction("Index", "Cart");
        }
    }
}
