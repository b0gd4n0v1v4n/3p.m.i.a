using System.Windows;
using AIMP_v3._0.ViewModel;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Interaction logic for LegalPersonInfoView.xaml
    /// </summary>
    public partial class LegalPersonInfoView : Window
    {
        public LegalPersonInfoView(LegalPersonViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
