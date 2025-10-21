using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GroupCalendar.Main.Converter
{
    public class TypeEqualToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value.GetType();

            return value.GetType() == parameter ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
