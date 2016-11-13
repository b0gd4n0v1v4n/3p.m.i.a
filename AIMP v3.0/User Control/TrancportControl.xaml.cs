using AIMP_v3._0.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AIMP_v3._0.View;
using AIMP_v3._0.ViewModel;
using System;
using Entities;

namespace AIMP_v3._0.User_Control
{
    /// <summary>
    /// Interaction logic for TrancportControl.xaml
    /// </summary>
    public partial class TrancportControl : UserControl, INotifyPropertyChanged, IObjectSetValue
    {
        private static void _TrancportChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as TrancportControl;

            control.OnPropertyChanged("TrancportSignature");
        }

        public TrancportControl()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static readonly DependencyProperty TrancportSignatureProperty =
    DependencyProperty.Register("TrancportSignature", typeof(string), typeof(UserFileControl),
        new UIPropertyMetadata(string.Empty, _TrancportSignatureChanged));

        private static void _TrancportSignatureChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as TrancportControl).OnPropertyChanged("TrancportSignature");
        }
        public string TrancportSignature
        {
            get
            {
                return
                    Trancport != null
                    ? $"Марка, модель: {Trancport.Make?.Name}, {Trancport.Model?.Name}"
                    :
                    string.Empty;
            }
            set
            {
                SetValue(TrancportSignatureProperty, value);
            }
        }

        public static readonly DependencyProperty TrancportProperty =
            DependencyProperty.Register("Trancport", typeof(Trancport), typeof(TrancportControl),
                new UIPropertyMetadata(null, _TrancportChanged));

        public Trancport Trancport
        {
            get { return (Trancport)GetValue(TrancportProperty); }
            set
            {
                SetValue(TrancportProperty, value);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Trancport == null)
                    Trancport = new Trancport();

                TrancportInfoViewModel viewModel = new TrancportInfoViewModel(Trancport, this);

                TrancportInfoView view = new TrancportInfoView(viewModel);

                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Не удалось открыть информацию о ТС");
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchTrancportViewModel searchTrancportViewModel = new SearchTrancportViewModel();

                SearchTrancportView searchContractorView = new SearchTrancportView(searchTrancportViewModel);

                if (searchContractorView.ShowDialog() == true)
                    Trancport = searchTrancportViewModel.CurrentTrancport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Trancport = null;
            OnPropertyChanged("TrancportSignature");
        }

        public void SetValue(object obj)
        {
            SetValue(TrancportProperty, obj);
        }
    }
}
