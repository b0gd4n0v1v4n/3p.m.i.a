using Aimp.Model.ReportOfClient;
using AIMP_v3._0.PerfectListView;

namespace AIMP_v3._0.ViewModel.Pages.ReportOfClient
{
    public class ClientReportListItemViewModel : ClientReportListItem,IFilterRow
    {
        public bool IsVisible { get; set; }
        public bool IsSelected { get; set; }
    }
}
