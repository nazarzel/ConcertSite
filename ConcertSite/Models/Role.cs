using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConcertSite.Models
{
    public class Role
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required, StringLength(maximumLength: 250)]
        public string name { get; set; }
        public virtual ICollection<User> users { get; set; }
    }
}