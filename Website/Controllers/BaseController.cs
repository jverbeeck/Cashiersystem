using System.Web.Mvc;
using BLL.Extensions;
using Website.Models;

namespace Website.Controllers
{
    /// <summary>
    /// A base class used for determining wether the user is authenticated
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// A protected property to determine if the user is authenticated used by the child classes 
        /// </summary>
        protected AuthEntity _auth => CheckAuth();

        /// <summary>
        /// A method that checks wether the user is authenticated via sessions.
        /// It also determines the user role
        /// </summary>
        /// <returns></returns>
        protected AuthEntity CheckAuth()
        {
            var entity = new AuthEntity();

            var current = System.Web.HttpContext.Current;
            var authorized = current.Session["authorized"];

            if (authorized == null || !(bool) authorized) return entity;
            entity.IsAuthenticated = true;
            var user = (User)current.Session["currentUser"];
            if (user != null)
            {
                entity.IsAdmin = user.UserRole == (int) Enums.UserRole.Administrator;
            }
            return entity;
        }
    }
}
