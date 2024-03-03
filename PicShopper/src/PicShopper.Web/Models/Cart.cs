using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicShopper.Web.Models
{
    public class Cart
    {
        public string ItemName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
