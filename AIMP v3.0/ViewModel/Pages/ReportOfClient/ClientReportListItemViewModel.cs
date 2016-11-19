using Aimp.Model.ReportOfClient;
using AIMP_v3._0.PerfectListView;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AIMP_v3._0.ViewModel.Pages.ReportOfClient
{
    public class ClientReportListItemViewModel : ClientReportListItem, IFilterRow, INotifyPropertyChanged
    {
        private bool _isVisible;
        public bool IsVisible { get { return _isVisible; }
            set {
                _isVisible = value;
                OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
