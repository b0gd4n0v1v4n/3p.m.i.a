using System.Threading;
using System.Windows;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Interaction logic for LoadingView.xaml
    /// </summary>
    public partial class LoadingView : Window
    {
        public LoadingView(string text)
        {
            InitializeComponent();
            DataContext = text;
        }
    }
}
