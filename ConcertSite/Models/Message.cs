using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConcertSite.Models
{
    public class Message
    {
        [Key]
        [Required]
        public int id { get; set; }
        public string loginFrom { get; set; }
        public string loginTo { get; set; }
        public string Text { get; set; }
        public DateTime time { get; set; }
    }
}