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
    public class BuyController : Controller
    {
        private ICartData _cartData;
        public BuyController(ICartData cartData)
        {
            _cartData = cartData;
        }

        [HttpPost]
        public IActionResult Index([FromForm] string title, string price)
        {
            List<BuyItem> items = new List<BuyItem>();
            var item            = new BuyItem();
            item.Name           = title;
            item.Price          = Convert.ToInt32(price);
            item.Count          = 1;
            items.Add(item);

            var model = new BuyItemViewModel();
            model.BuyItems = items;
            return View(model);
        }

        [HttpPost]
        public IActionResult CheckOut()
        {
            string id = User.Identities.First().Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault();

            int userId = Convert.ToInt32(id);
            var model  = new BuyItemViewModel();

            model.BuyItems = _cartData.GetBuyItems(userId);
            return View(model);
        }
    }
}
