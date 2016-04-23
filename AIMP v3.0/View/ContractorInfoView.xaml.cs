using System.Windows;
using AIMP_v3._0.ViewModel;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Interaction logic for ContractorInfoView.xaml
    /// </summary>
    public partial class ContractorInfoView : Window
    {
        public ContractorInfoView(ContractorInfoViewModel contractorInfoViewModel)
        {
            InitializeComponent();

            DataContext = contractorInfoViewModel;
        }
    }
}
