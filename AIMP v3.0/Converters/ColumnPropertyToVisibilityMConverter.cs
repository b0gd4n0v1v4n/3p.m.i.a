using System;
using System.Windows.Data;
using System.Windows;
using System.ComponentModel;


namespace AIMP_v3._0.Converters
{
    public class ColumnPropertyToVisibilityMConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var direction = parameter as String == "asc" ? ListSortDirection.Ascending : ListSortDirection.Descending;

                return
                    values[0] as String == values[1] as String &&
                    (ListSortDirection)values[2] == direction ? Visibility.Visible : Visibility.Collapsed;
            }
            catch
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
