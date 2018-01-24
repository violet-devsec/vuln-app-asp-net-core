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
            }
            else
            {
                _retUrl = "User";
            }

            model.RetUrl = _retUrl;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login model)
        {
            int success = _userData.DoLogin(model.UserName, model.Password);
            string rUrl = HttpContext.Request.Path;

            rUrl = Url.ToString();

            if (success != 0)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Sid, success.ToString())
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                
                //await HttpContext.Authentication.SignInAsync("CookieAuthentication", principal);
                await HttpContext.SignInAsync(principal);
                
                return RedirectToAction("Index", model.RetUrl);
            }

            return View();
        }
    }
}
