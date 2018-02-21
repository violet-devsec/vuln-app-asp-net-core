using PicShopper.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicShopper.Web.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<Cart> CartItems { get; set; }
    }
}
