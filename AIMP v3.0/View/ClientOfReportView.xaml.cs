using AIMP_v3._0.ViewModel.ClientOfReport;
using System.Windows;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Interaction logic for ReportOfClientView.xaml
    /// </summary>
    public partial class ReportOfClientView : Window
    {
        public ReportOfClientView(ClientReportViewModel model)
        {
            InitializeComponent();

            DataContext = model;
        }
    }
}
