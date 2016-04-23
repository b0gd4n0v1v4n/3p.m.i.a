using AIMP_v3._0.ViewModel;
using System.Windows;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Логика взаимодействия для SearchTrancportView.xaml
    /// </summary>
    public partial class SearchTrancportView : Window
    {
        public SearchTrancportView(SearchTrancportViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
