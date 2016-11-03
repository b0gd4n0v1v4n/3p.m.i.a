using System.Collections.Generic;

namespace Aimp.Model.CardTrancports
{
    public class CardTrancportsDto
    {
        public IEnumerable<CardTrancportListItemDto> Items { get; set; }
        public string[] StatusesCardForFilerStart { get; set; }
    }
}