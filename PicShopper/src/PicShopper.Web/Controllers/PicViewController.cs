using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicShopper.Web.Models;
using PicShopper.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicShopper.Web.Controllers
{
    [Authorize]
    public class PicViewController : Controller
    {
        private IPicViewData _picData;

        public PicViewController(IPicViewData picData)
        {
            _picData = picData;
        }

        //** IDOR Vulnerabiltiy **//
        public ActionResult Index([FromQuery] int picId)
        {
            PicView model = new PicView();

            model    = _picData.GetPic(picId);
            model.Id = picId;

            return View(model);
        }
    }
}
