using AIMP_v3._0.View;
using System;
using System.Windows;
using System.Windows.Threading;

namespace AIMP_v3._0.Helpers
{
    public static class LoadingViewHalper
    {
        public static LoadingView View { get; private set; }

        public static void ShowDialog(string text,Action process)
        {
            View?.Close();
            View = new LoadingView(text);
            Application.Current.Dispatcher.BeginInvoke(
            DispatcherPriority.Background,
            new Action(() => 
            {
                process();
                View?.Close();
            }));
            View.ShowDialog();
        }
    }
}
