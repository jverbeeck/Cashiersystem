using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Extensions;
using DAL;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : BaseController
    {
        private readonly List<User> _users = DataAccess.GetAllUsers();

        public ActionResult Index()
        {
            if (_auth.IsAuthenticated)
            {
                var user = (User)System.Web.HttpContext.Current.Session["currentUser"];
                if (user != null)
                    return View("LandingPage", user);
            }
            return View(_users);
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var user = new User { FirstName = username, Password = password };
                var returnUser = DataAccess.CheckIfUserExists(user);
                if (returnUser != null)
                {
                    System.Web.HttpContext.Current.Session["authorized"] = true;
                    System.Web.HttpContext.Current.Session["currentUser"] = returnUser;
                    if (returnUser.UserRole == (int)Enums.UserRole.User)
                    {
                        return View("~/Views/Orders/index.cshtml");
                    }
                    return View("LandingPage");
                }
            }
            ModelState.AddModelError("MessageError", "Credentials are incorrect");
            return View("Index", _users);
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon();
            return View("Index", _users);
        }
    }
}