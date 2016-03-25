using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConcertSite.Models
{
    public class User
    {
        [Key]
        [Required]
        public int id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string pass { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public int phone { get; set; }
        public string email { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<StatementOnBuy> StatementOnBuy { get; set; }
    }
}