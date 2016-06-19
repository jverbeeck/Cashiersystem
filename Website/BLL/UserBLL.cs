using System.Collections.Generic;
using System.Web.Mvc;
using Website.Models;

namespace Website.BLL
{
    /// <summary>
    /// This class handles all the logic for the users
    /// </summary>
    public class UserBLL
    {
        /// <summary>
        /// This method returns the mapping for the userroles
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// This method determines the userrole's name
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This method adds a salt to a given user
        /// </summary>
        /// <param name="user"></param>
        public static void AddSalt(User user)
        {

            var keyNew = Helpers.GeneratePassword(10);
            var password = Helpers.EncodePassword(user.Password, keyNew);
            user.Salt = keyNew;
            user.CheckSum = password;
            user.Password = string.Empty;
        }

        /// <summary>
        /// This method decodes the salt for a given user and compares it with the hash retrieved from the database object
        /// </summary>
        /// <param name="dbUser"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool DecodeSalt(User dbUser, User user)
        {
            //Password Hasing Process Call Helper Class Method    
            var encodingPasswordString = Helpers.EncodePassword(user.Password, dbUser.Salt);

            return encodingPasswordString.Equals(dbUser.CheckSum);
        }
    }
}