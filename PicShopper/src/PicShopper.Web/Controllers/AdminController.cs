using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicShopper.Web.Models;
using PicShopper.Web.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PicShopper.Web.Controllers
{
    public class AdminController : Controller
    {
        private IUserData _userData;
        public AdminController(IUserData userData)
        {
            _userData = userData;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            Login user  = _userData.DoAdminLogin(model.UserName, model.Password);
            string rUrl = HttpContext.Request.Path;
            string Role = "Admin";

            rUrl = Url.ToString();

            if (user.UserName != "" && user.UType == UserType.Admin)
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

                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        public IActionResult AddUser()
        {
            return View();
        }

        //*** No user role check : Authorization Bypass ***//
        [HttpPost]
        public IActionResult AddUser(AddUser model)
        {
            var newUser       = new AddUser();
            newUser.FirstName = model.FirstName;
            newUser.LastName  = model.LastName;
            newUser.LoginName = model.LoginName;
            newUser.Dollors   = model.Dollors;

            bool success = _userData.AddUser(newUser);

            if (success)
            {
                ViewBag.Message = "User Added!";
                return View();
            }

            else
            {
                ViewBag.Message = "Error in adding user";
                return View();
            }
        }
    }
}
