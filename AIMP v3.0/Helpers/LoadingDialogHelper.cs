using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using AIMP_v3._0.View;
using AIMP_v3._0.ViewModel;

namespace AIMP_v3._0.Helpers
{
    public static class LoadingDialogHelper
    {
        private static LoadingView _view;

        public static void Show(string message)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            LoadingDialogViewModel loadingDialogViewModel = new LoadingDialogViewModel();

                            loadingDialogViewModel.DisplayName = message;

                            _view = new LoadingView(loadingDialogViewModel);

                            _view.ShowDialog();
                        }));
        }

        public static void Hide()
        {
         _view.Close();   
        }
    }
}
