using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.BLL;
using Website.DAL;
using Website.Models;

namespace DAL
{
    public static class DataAccess
    {
        private static readonly CashierContext _context = new CashierContext();


        public static User CheckIfUserExists(User user)
        {
            var query = _context.Users.FirstOrDefault(w => w.FirstName.Equals(user.FirstName));
            var isCorrectPassword = UserBLL.DecodeSalt(query, user);

            return isCorrectPassword ? query : null;
        }

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

        public static List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public static List<StockItem> GetStockItems()
        {
            return _context.StockItems.ToList();
        }
    }
}
