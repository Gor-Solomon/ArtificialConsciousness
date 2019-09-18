using System;
using System.Globalization;
using System.Windows.Data;

namespace AC.Desktop.Controls.Converters
{
    public class EqualityComparer : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var text1 = values[0]?.ToString();
            var text2 = values[1]?.ToString();

            int index;

            int.TryParse(text1, out index);

            int copyFrom;

            int.TryParse(text2, out copyFrom);

            if (index == 0 || copyFrom == 0)

                return text1 != text2;

            return copyFrom < index;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
