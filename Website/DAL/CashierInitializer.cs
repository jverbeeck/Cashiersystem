using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Extensions;
using Website.Models;

namespace Website.DAL
{
    public class CashierInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CashierContext>
    {
        protected override void Seed(CashierContext context)
        {
            var users = new List<User>
            {
            new User
            {
                FirstName= "Jonas",
                LastName ="Verbeeck",
                Email = "admin@mail.com",
                Password = "admin",
                UserRole = (int) Enums.UserRole.Administrator
            }};

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

        }
    }
}