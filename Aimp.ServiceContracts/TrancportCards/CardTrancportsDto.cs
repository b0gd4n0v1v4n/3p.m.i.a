﻿using System.Collections.Generic;

namespace Aimp.ServiceContracts.TrancportCards
{
    public class CardTrancportsDto
    {
        public IEnumerable<CardTrancportListItemDto> Items { get; set; }
        public string[] StatusesCardForFilerStart { get; set; }
    }
}
