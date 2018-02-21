using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using PicShopper.Web.Models;
using PicShopper.Web.Services;
using Microsoft.AspNetCore.Authentication;
using System.Threading;

namespace PicShopper.Web.Controllers
{
    public class LoginController : Controller
    {
        private IUserData _userData;
        private string _retUrl = "";

        private string ParseQueryStr(string qString)
        {
            string value = "";
            foreach (string item in qString.Split('&'))
            {
                string[] parts = item.Replace('?', ' ').Replace('/', ' ').Split('=');
                if (parts[0].Trim() == "ReturnUrl")
                {
                    value = parts[1].Trim();
                    break;
                }
            }

            return value;
        }

        public LoginController(IUserData userData)
        {
            _userData = userData;
        }

        public IActionResult Index()
        {
            Login model = new Login();

            if (HttpContext.Request.QueryString.Value != "")
            {
                _retUrl = ParseQueryStr(HttpUtility.UrlDecode(HttpContext.Request.QueryString.ToString()));
                
                if(_retUrl == "Admin")
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            else
            {
                _retUrl = "User";
            }

            model.RetUrl = _retUrl;

            return View(model);
        }
        
    }
}
