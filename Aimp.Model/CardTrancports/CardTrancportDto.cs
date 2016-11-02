using Aimp.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aimp.Model.CardTrancports
{
    public class CardTrancportDto : IResponse
    {
        public CardTrancportDocument Document { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
