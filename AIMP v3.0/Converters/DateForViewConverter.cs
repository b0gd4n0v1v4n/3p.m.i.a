using Aimp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AIMP_v3._0.Converters
{
    public class DateForViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? date = value as DateTime?;

            if (date == null)
                return value;

            if (!date.HasValue)
                return string.Empty;

            if (date.Value == DateTime.MinValue)
                return string.Empty;

            return date.Value.ToString(DataFormats.DateFormat);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
