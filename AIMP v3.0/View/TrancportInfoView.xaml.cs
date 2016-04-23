using AIMP_v3._0.ViewModel;
using System.Windows;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Interaction logic for TrancportInfoView.xaml
    /// </summary>
    public partial class TrancportInfoView : Window
    {
        public TrancportInfoView(TrancportInfoViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
