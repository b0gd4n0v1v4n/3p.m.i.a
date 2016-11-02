using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities
{
    public class PreCheckCardTrancport : Entity
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public decimal Summ { get; set; }
        public decimal PriceForClient { get; set; }
        public bool Paid { get; set; }
        public bool Card { get; set; }
        [ForeignKey("CardTrancport")]
        public int CardTrancportId { get; set; }
        public virtual CardTrancport CardTrancport { get; set; }
    }
}
