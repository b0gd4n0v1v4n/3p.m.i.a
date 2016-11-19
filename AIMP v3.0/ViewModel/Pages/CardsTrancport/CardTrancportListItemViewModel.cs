using AIMP_v3._0.Model;
using AIMP_v3._0.PerfectListView;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AIMP_v3._0.ViewModel.Pages.CardsTrancport
{
    public class CardTrancportListItemViewModel : Identity,IFilterRow,INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
