using Microsoft.AspNetCore.Mvc;
using PicShopper.Web.Models;
using PicShopper.Web.Services;
using PicShopper.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var model       = new CartViewModel();
            model.CartItems = _cartData.GetCart(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart([FromForm] string title, string price, string count)
        {
            bool   success = false;
            string userId  = User.Identities.First().Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault();

            var cart = new Cart();
            cart.ItemName = title;
            cart.Price    = Convert.ToInt32(price);
            cart.Count    = Convert.ToInt32(count);
            success       = _cartData.AddToCart(cart, userId);

            if(success)
            {
                return RedirectToAction("Index", new { id = userId });
            }
            else
            {
                return RedirectToAction("Index", "Recent");
            }            
        }
    }
}
