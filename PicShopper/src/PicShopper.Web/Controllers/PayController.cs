using Microsoft.AspNetCore.Mvc;
using PicShopper.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PicShopper.Web.Controllers
{
    public class PayController : Controller
    {
        private ICartData _cartData;

        public PayController(ICartData cartData)
        {
            _cartData = cartData;
        }

        [HttpPost]
        public IActionResult Index()
        {
            string id = User.Identities.First().Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault();

            int userId = Convert.ToInt32(id);

            _cartData.RemoveItem(userId);

            return View();
        }
    }
}
