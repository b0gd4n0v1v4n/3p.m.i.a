
using System.Windows;
using AIMP_v3._0.ViewModel;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Interaction logic for LoadingView.xaml
    /// </summary>
    public partial class LoadingView : Window
    {
        public LoadingView(LoadingDialogViewModel loadingModalDialogViewModel)
        {
            DataContext = loadingModalDialogViewModel;
            loadingModalDialogViewModel.EndProgress += () =>
            {
                Close();
            };
            InitializeComponent();
        }

        public LoadingView()
        {
            InitializeComponent();
        }
    }
}
