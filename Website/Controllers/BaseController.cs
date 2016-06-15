using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Extensions;
using Website.BLL;
using Website.DAL;
using Website.Models;

namespace Website.Controllers
{
    public class BaseController : Controller
    {
        protected AuthEntity _auth => CheckAuth();

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
