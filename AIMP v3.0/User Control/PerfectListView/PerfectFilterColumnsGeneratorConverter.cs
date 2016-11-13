using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AIMP_v3._0.PerfectListView
{
    public class PerfectFilterColumnsGeneratorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var columnName = value as string;
            if (columnName != null)
            {
                var grdiView = new GridView();

                Window window = Application.Current.MainWindow;
                DataTemplate isSelectedColumnTemplate = (DataTemplate)window.FindResource("IsSelectedCheckBoxCellTemplate");

                var isSelectedColumn = new System.Windows.Controls.GridViewColumn
                {
                    Header = " ",
                    CellTemplate = isSelectedColumnTemplate
                };

                grdiView.Columns.Add(isSelectedColumn);

                var textColumn = new System.Windows.Controls.GridViewColumn
                {
                    Header = "",
                    DisplayMemberBinding = new Binding(columnName)
                };
                grdiView.Columns.Add(textColumn);

                return grdiView;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}