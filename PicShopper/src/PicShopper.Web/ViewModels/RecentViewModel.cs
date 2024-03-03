using System.Collections.Generic;
using PicShopper.Web.Models;

namespace PicShopper.Web.ViewModels
{
    public class RecentViewModel
    {
        public IEnumerable<RecentFile> RecentFiles {get; set;}
    }
}
