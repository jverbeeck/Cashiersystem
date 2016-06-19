using System.Collections.Generic;
using System.Web.Mvc;
using BLL.Extensions;
using DAL;
using Website.Models;

namespace Website.Controllers
{
    /// <summary>
    /// The main controller used for login and logout
    /// </summary>
    public class HomeController : BaseController
    {
        private readonly List<User> _users = DataAccess.GetAllUsers();

        /// <summary>
        /// Returns the first view an user encounters and lists the user in the database
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Log a user in if provided with the correct credentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
                        return RedirectToAction("List","Orders");
                    }
                    return View("LandingPage");
                }
            }
            ModelState.AddModelError("MessageError", "Credentials are incorrect");
            return View("Index", _users);
        }

        /// <summary>
        /// Logs a user out
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon();
            return View("Index", _users);
        }
    }
}