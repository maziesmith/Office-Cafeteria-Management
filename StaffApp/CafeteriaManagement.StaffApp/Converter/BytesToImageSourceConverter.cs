using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CafeteriaManagement.StaffApp.Converter
{
    public class BytesToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var biImg = new BitmapImage();
            var ms = new MemoryStream(value as byte[] ?? throw new InvalidOperationException());
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
            return biImg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
