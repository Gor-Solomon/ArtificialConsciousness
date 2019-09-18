using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AC.Desktop.Controls.Converters
{
    public class BoolToCollapseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool flag)
                return flag ? Visibility.Visible : Visibility.Collapsed;
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
