using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PicShopper.Web.Models;
using PicShopper.Web.Services;
using PicShopper.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PicShopper.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserData _userData;
        private ICartData _cartData;
        public UserController(IUserData userData, ICartData cartData)
        {
            _userData = userData;
            _cartData = cartData;
        }
        public IActionResult Index()
        {
            var model = new UserViewModel();
            model.Name = User.Identities.First().Claims.Where(c => c.Type == ClaimTypes.Name)
                   .Select(c => c.Value).SingleOrDefault();
            model.Id = User.Identities.First().Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault();
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            Login user = _userData.DoLogin(model.UserName, model.Password);
            string rUrl = HttpContext.Request.Path;
            string Role = "User";

            rUrl = Url.ToString();

            if (user.UserName != "" && user.UType == UserType.User)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, Role)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                //await HttpContext.Authentication.SignInAsync("CookieAuthentication", principal);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", model.RetUrl);
            }

            return View();
        }

        public IActionResult ChangePassword()
        {
            ChangePasswordModel model = new ChangePasswordModel();

            model.UserId = User.Identities.First().Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault();

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel data)
        {
            ChangePasswordModel model = new ChangePasswordModel();

            model.UserId = User.Identities.First().Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault();

            bool passChanged = _userData.ChangePassword(data.UserId, data.NewPassword);

            return View(model);
        }

    }
}
