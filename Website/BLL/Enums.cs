namespace BLL.Extensions
{
    /// <summary>
    /// A class for all the Enums
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// A userRole Enum
        /// </summary>
        public enum UserRole
        {
            /// <summary>
            /// The admin
            /// </summary>
            Administrator,
            /// <summary>
            /// A regular User
            /// </summary>
            User
        }

        /// <summary>
        /// A Enum to determine the category of the drink
        /// </summary>
        public enum DrinkCategory
        {
            /// <summary>
            /// Alcohol
            /// </summary>
            Alcohol = 1,
            /// <summary>
            /// Non-Alcohol
            /// </summary>
            NonAlcohol = 2
        }

        /// <summary>
        /// Determines the scenario of the order
        /// </summary>
        public enum Scenario
        {
            /// <summary>
            /// Create
            /// </summary>
            Create = -1,
            /// <summary>
            /// Update
            /// </summary>
            Update = -2,
            /// <summary>
            /// Confirm
            /// </summary>
            Confirm = -5
        }

        /// <summary>
        /// A enum for special cases
        /// </summary>
        public enum Special
        {
            /// <summary>
            /// Table number
            /// </summary>
            TableNumber = -4,
            /// <summary>
            /// Order ID
            /// </summary>
            OrderId = -3
        }
    }
}
