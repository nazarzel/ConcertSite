using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace ConcertSite.Models
{
    public class DbInitialize : DropCreateDatabaseAlways<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            //context.Users.Add(
            //    new User() { city="hello" });
            //context.Bilets.Add(
            //    new Bilet() { price = 500 });
            //context.Concerts.Add(
            //    new Concert() { city = "hello" });
            //context.UsersInRoles.Add(
            //    new UserInRole() { role = "admin" });
            //context.StatementsOnBuy.Add(
            //    new StatementOnBuy() { isPaid = false });

            base.Seed(context);
        }
    }
}