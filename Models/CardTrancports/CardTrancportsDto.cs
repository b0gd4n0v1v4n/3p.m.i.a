using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.CardTrancports
{
    public class CardTrancportsDto : IResponse
    {
        public bool Error
        {
            get; set;
        }

        public IEnumerable<CardTrancportListItemDto> Items { get; set; }
        public string[] StatusesCardForFilerStart { get; set; }
        public string Message
        {
            get; set;
        }
    }
}
