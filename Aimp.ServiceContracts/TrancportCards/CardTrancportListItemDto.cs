using System;

namespace Aimp.ServiceContracts.TrancportCards
{
    public class CardTrancportListItemDto : Identity
    {
        public string Number {get;set;}
        public DateTime DateStart { get; set; }
        public string NumberT { get; set; }
        public string MakeModelTrancport { get; set; }
        public string YearTrancport { get; set; }
        public string ColorTrancport { get; set; }
        public string Price { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public DateTime? DateSale {get;set;}
        public string Manager {get;set;}
        public string User {get;set;}
    }
}
