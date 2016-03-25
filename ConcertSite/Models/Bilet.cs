using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConcertSite.Models
{
    public class Bilet
    {
        [Key]
        [Required]
        public int id { get; set; }
        public int price { get; set; }
        public bool isFree { get; set; }
        public int sektor { get; set; }
        public int row { get; set; }
        public int place { get; set; }
        public int ConcertId { get; set; }
        public Guid guidBilet { get; set; }
        public virtual Concert Concert { get; set; }
        public virtual ICollection<StatementOnBuy> StatementObBuy { get; set; }
        //public List<StatementOnBuy> statementsOnBuy { get; set; }
    }
}