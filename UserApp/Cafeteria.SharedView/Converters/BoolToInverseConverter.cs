using System;
using System.Globalization;
using Xamarin.Forms;

namespace Cafeteria.SharedView.Converters
{
    public class BoolToInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isTrue = (bool) value;
            return !isTrue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}