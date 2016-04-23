using AIMP_v3._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.CardTrancports
{
    public class CardTrancportListItemDto : Identity
    {
        public string Status { get; set; }
        public string Number {get;set;}
        public string NumberT {get;set;}
        public DateTime DateStart { get; set; }
        public DateTime? DateSale {get;set;}
        public string MakeModelTrancport {get;set;}
        public string YearTrancport {get;set;}
        public string ColorTrancport { get; set; }
        public string Vin {get;set;}
        public string Pts {get;set;}
        public string Keys {get;set;}
        public string Price {get;set;}
        public string OwnerAndTelefon{get;set;}
        public string Manager {get;set;}
        public string User {get;set;}
}
}
