using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
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
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            DataContext = text;

            //double maxWidth = 0;
            //double maxHeight = 0;

            //double left = 10000;
            //double top = 10000;

            //foreach (Window iWindow in Application.Current.Windows)
            //{
            //    if (maxWidth < iWindow.ActualWidth)
            //        maxWidth = iWindow.ActualWidth;

            //    if (maxHeight < iWindow.ActualHeight)
            //        maxHeight = iWindow.ActualHeight;

            //    if (left > iWindow.LayoutTransform.)
            //        left = iWindow.Left;

            //    if (top > iWindow.Top)
            //        top = iWindow.Top;
            //}

            //Top = top;
            //Left = left;

            //Width = maxWidth;
            //Height = maxHeight;
        }
    }
}
