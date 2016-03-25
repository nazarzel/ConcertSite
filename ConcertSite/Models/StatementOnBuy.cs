using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ConcertSite.Models
{
    public class StatementOnBuy
    {
        [Key]
        [Required]
        public int id { get; set; }
        public bool isPaid { get; set; }
        public virtual int UserId { get; set; }
        public virtual int BiletId { get; set; }

        public virtual User User { get; set; }
        public virtual Bilet Bilet { get; set; }
        //public List<User> users { get; set; }
    }
}