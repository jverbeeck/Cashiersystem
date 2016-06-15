using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.BLL
{
    public class UserBLL
    {
        public static List<SelectListItem> GetUserRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Administator",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "User",
                    Value = "1"
                }
            };
        }

        public static string GetUserRoleName(int userRole)
        {
            switch (userRole)
            {
                case 0:
                    return "Administrator";
                case 1:
                    return "User";
                default:
                    return string.Empty;
            }
        }

        public static void AddSalt(User user)
        {

            var keyNew = Helpers.GeneratePassword(10);
            var password = Helpers.EncodePassword(user.Password, keyNew);
            user.Salt = keyNew;
            user.CheckSum = password;
            user.Password = string.Empty;
        }

        public static bool DecodeSalt(User dbUser, User user)
        {
            //Password Hasing Process Call Helper Class Method    
            var encodingPasswordString = Helpers.EncodePassword(user.Password, dbUser.Salt);

            return encodingPasswordString.Equals(dbUser.CheckSum);
        }
    }
}