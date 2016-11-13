﻿using AIMP_v3._0.Model;
using AIMP_v3._0.PerfectListView;

namespace AIMP_v3._0.ViewModel.Pages.CardsTrancport
{
    public class CardTrancportListItemViewModel : Identity,IFilterRow
    {
        public string Number { get; set; }
        public string DateStart { get; set; }
        public string NumberT { get; set; }
        public string MakeModelTrancport { get; set; }
        public string YearTrancport { get; set; }
        public string ColorTrancport { get; set; }
        public string Price { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public string DateSale { get; set; }
        public string Manager { get; set; }
        public string User { get; set; }
        public bool IsVisible { get; set; }
        public bool IsSelected { get; set; }
    }
}
