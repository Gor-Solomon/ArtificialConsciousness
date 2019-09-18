using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AC.Desktop.Controls.Converters
{
    public class SourceToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage biImg = new BitmapImage();
            if (value is string path && File.Exists(path))
            {
                var bytes = File.ReadAllBytes(path);
                var stream = new MemoryStream(bytes);

                biImg.BeginInit();
                biImg.StreamSource = stream;
                biImg.EndInit();
            }
            return biImg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
