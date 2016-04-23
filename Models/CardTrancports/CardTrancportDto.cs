using Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.CardTrancports
{
    public class CardTrancportDto : IResponse
    {
        public CardTrancportDocument Document { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
