using System.Collections.Generic;
using PicShopper.Web.Models;

namespace PicShopper.Web.ViewModels
{
    public class GuestBookViewModel
    {
        public IEnumerable<GuestBook> Comments { get; set; }
    }
}
