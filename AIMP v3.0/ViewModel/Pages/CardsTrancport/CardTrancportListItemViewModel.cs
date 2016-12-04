using AIMP_v3._0.Model;
using AIMP_v3._0.PerfectListView;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AIMP_v3._0.ViewModel.Pages.CardsTrancport
{
    public class CardTrancportListItemViewModel : Identity,IFilterRow,INotifyPropertyChanged
    {
        public int Number { get; set; }
        public DateTime DateStart { get; set; }
        public int NumberT { get; set; }
        public string MakeModelTrancport { get; set; }
        public string YearTrancport { get; set; }
        public string ColorTrancport { get; set; }
        public string Price { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public DateTime DateSale { get; set; }
        public string Manager { get; set; }
        public string User { get; set; }
        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }
        public int CommissionNumber { get; set; }
        public string Documents { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
