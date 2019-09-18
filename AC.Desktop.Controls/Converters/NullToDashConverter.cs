using System;
using System.Globalization;
using System.Windows.Data;

namespace AC.Desktop.Controls.Converters
{
    public class NullToDashConverter : IValueConverter
    {
        const string ReplaceValue = "-.--";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var originalValue = value as double?;

            if (originalValue.HasValue)
                return originalValue.Value;

            return ReplaceValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
