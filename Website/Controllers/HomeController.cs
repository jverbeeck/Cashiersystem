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
                var userExists = DataAccess.CheckIfUserExists(user);
                if (userExists)
                {
                    System.Web.HttpContext.Current.Session["authorized"] = true;
                    System.Web.HttpContext.Current.Session["currentUser"] = user;
                    //return RedirectToAction("LandingPage", user);
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
            return View("index", _users);
        }
    }
}