﻿using Microsoft.AspNetCore.Mvc;

namespace PicShopper.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View(); 
        }
    }
}
