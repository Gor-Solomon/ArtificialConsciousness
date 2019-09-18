using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AC.Desktop.Controls.Converters
{
    
    public class EnumToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color color = new Color();

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), value.ToString());

            if (parameterValue is Enum logLevel)
            {
                //switch (logLevel)
                //{
                //    case LogLevelEnum.Warning:
                //        color = Colors.Yellow;
                //        break;
                //    case LogLevelEnum.Error:
                //        color = Colors.Red;
                //        break;
                //    case LogLevelEnum.Initialize:
                //        color = Colors.Orange;
                //        break;
                //    case LogLevelEnum.Info:
                //        color = Colors.Black;
                //        break;
                //    case LogLevelEnum.Success:
                //        color = Color.FromRgb(90, 216, 58);
                //        break;
                //    default:
                //        color = Colors.Black;
                //        break;
                //}
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
