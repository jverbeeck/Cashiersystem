using System.Collections.Generic;
using BLL.Extensions;
using Website.Models;

namespace Website.DAL
{
    /// <summary>
    /// This class initializes the data form the database on first create
    /// </summary>
    public class CashierInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CashierContext>
    {
        /// <summary>
        /// A method that should be overridden to actually add data to the context for seeding.
        ///             The default implementation does nothing.
        /// Populate the database with initial data
        /// </summary>
        /// <param name="context">The context to seed. </param>
        protected override void Seed(CashierContext context)
        {
            var users = new List<User>
            {
            new User
            {
                FirstName= "Jonas",
                LastName ="Verbeeck",
                Email = "admin@mail.com",
                CheckSum = "12-DC-33-A7-CA-FF-E8-48-27-C6-43-EC-1F-BC-77-D7",
                Salt = "1huPWm9NsW",
                UserRole = (int) Enums.UserRole.Administrator
            }};

            var categories = new List<Category>
            {
                new Category { CategoryName = "Alcohol" },
                new Category { CategoryName = "Non-Alcohol" }
            };

            users.ForEach(s => context.Users.Add(s));
            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

        }
    }
}