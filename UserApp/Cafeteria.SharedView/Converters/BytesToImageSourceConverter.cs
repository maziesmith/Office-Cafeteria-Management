using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace Cafeteria.SharedView.Converters
{
    public class BytesToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ImageSource.FromStream(() => new MemoryStream((byte[])value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
