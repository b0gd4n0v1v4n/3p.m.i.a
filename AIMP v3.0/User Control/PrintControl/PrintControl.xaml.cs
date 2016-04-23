using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.ViewModel;

namespace AIMP_v3._0.User_Control
{
    /// <summary>
    /// Логика взаимодействия для PrintControl.xaml
    /// </summary>
    public partial class PrintControl : UserControl, INotifyPropertyChanged
    {
        private static void _TemplatesChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as PrintControl).OnPropertyChanged("ReportTemplatesProperty");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PrintControl()
        {
            InitializeComponent();
        }

        public List<PrintItem> ReportTemplates
        {
            get { return (List<PrintItem>)GetValue(ReportTemplatesProperty); }
            set { SetValue(ReportTemplatesProperty, value); }
        }

        public static readonly DependencyProperty ReportTemplatesProperty =
                        DependencyProperty.Register("ReportTemplates", typeof(List<PrintItem>), typeof(PrintControl),
                                new UIPropertyMetadata(null, _TemplatesChanged));

        public Command PrintCommand
        {
            get
            {
                return new Command((printItem) =>
                {
                    try
                    {
                        var item = printItem as PrintItem;
                        using (AimpService service = new AimpService())
                        {
                            var response = service.GetPrintedDocument(item.Type, item.Name,item.Document.Id);
                            if(response.Error)
                                throw new Exception(response.Message);
                            OpenUserFile.Open(item.Name,response.Document.File);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка");
                    }
                });
            }
        }
    }
}
