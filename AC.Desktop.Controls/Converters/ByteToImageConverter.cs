using System;
using System.IO;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace AC.Desktop.Controls.Converters
{
    public class ByteToImageConverter : IValueConverter
    {
        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream memStream = new MemoryStream(imageByteArray);

            biImg.BeginInit();
            biImg.StreamSource = memStream;
            biImg.EndInit();

            return biImg;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage img = null;
            if (value != null)
            {
                var imgByte = value as byte[];
                if (imgByte?.Length > 0)
                    img = this.ConvertByteArrayToBitMapImage(imgByte);
            }
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
