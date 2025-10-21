using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GroupCalendar.Main.Converter
{
    public class TypeEqualToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value.GetType();

            return value.GetType() == parameter ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
