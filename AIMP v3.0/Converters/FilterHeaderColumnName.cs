using System;
using System.Globalization;
using System.Windows.Data;

namespace AIMP_v3._0.Converters
{
    public class FilterHeaderColumnName : IValueConverter
    {
        public const char Seporator = '|';

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                if (value.ToString().IndexOf(Seporator) > 0)
                    return value.ToString().Split(Seporator)[0];
                else
                    return value;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
