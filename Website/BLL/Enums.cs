using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions
{
    public class Enums
    {
        public enum UserRole
        {
            Administrator,
            User
        }

        public enum DrinkCategory
        {
            Alcohol = 1,
            NonAlcohol = 2
        }

        public enum Scenario
        {
            Create = -1,
            Update = -2,
            Confirm = -5
        }

        public enum Special
        {
            TableNumber = -4,
            OrderId = -3
        }
    }
}
