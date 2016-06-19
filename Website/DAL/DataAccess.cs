using System;
using System.Collections.Generic;
using System.Linq;
using Website.BLL;
using Website.DAL;
using Website.Models;

namespace DAL
{
    /// <summary>
    /// A class for interactions with the database
    /// </summary>
    public static class DataAccess
    {
        private static readonly CashierContext _context = new CashierContext();


        /// <summary>
        /// A method to check if the user exists
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static User CheckIfUserExists(User user)
        {
            var query = _context.Users.FirstOrDefault(w => w.FirstName.Equals(user.FirstName));
            var isCorrectPassword = UserBLL.DecodeSalt(query, user);

            return isCorrectPassword ? query : null;
        }

        /// <summary>
        /// Gets all the users from the database
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAllUsers()
        {
            var query = _context.Users.ToList();

            //make sure the loaded data contains no pwd!!
            foreach (var user in query)
            {
                user.Password = String.Empty;
            }
            return query;
        }

        /// <summary>
        /// Gets all the categories from the database
        /// </summary>
        /// <returns></returns>
        public static List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        /// <summary>
        /// Gets all the stockitems from the database 
        /// </summary>
        /// <returns></returns>
        public static List<StockItem> GetStockItems()
        {
            return _context.StockItems.ToList();
        }
    }
}
