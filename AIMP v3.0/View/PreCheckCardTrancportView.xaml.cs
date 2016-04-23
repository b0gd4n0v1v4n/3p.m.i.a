using AIMP_v3._0.ViewModel.CardsTrancport;
using System.Windows;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Interaction logic for PreCheckCardTrancportView.xaml
    /// </summary>
    public partial class PreCheckCardTrancportView : Window
    {
        public PreCheckCardTrancportView(PreCheckCardTrancportViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
