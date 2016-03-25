using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace ConcertSite.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("name=DefaultConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Bilet> Bilets { get; set; }
        public DbSet<StatementOnBuy> StatementsOnBuy { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}