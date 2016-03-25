using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConcertSite.Models
{
    public class Concert
    {
        [Key]
        [Required]
        public int id { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string place { get; set; }
        public DateTime time { get; set; }//!!!
        public string singer { get; set; }

        public virtual ICollection<Bilet> bilets { get; set; }
        //public List<Bilet> bilets { get; set; }
    }
}