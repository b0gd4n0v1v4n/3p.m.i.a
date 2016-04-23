using System.Windows;
using AIMP_v3._0.ViewModel;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Interaction logic for CashTransactionView.xaml
    /// </summary>
    public partial class CashTransactionView : Window
    {
        public CashTransactionView(CashTransactionViewModel cashTransaction)
        {
            InitializeComponent();
            DataContext = cashTransaction;
        }
    }
}
