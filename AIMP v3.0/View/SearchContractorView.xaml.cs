
using System.Windows;
using AIMP_v3._0.ViewModel;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Interaction logic for SearchContractorView.xaml
    /// </summary>
    public partial class SearchContractorView : Window
    {
        public SearchContractorView(SearchContractorViewModel searchContractorViewModel)
        {
            InitializeComponent();

            DataContext = searchContractorViewModel;
        }
    }
}
