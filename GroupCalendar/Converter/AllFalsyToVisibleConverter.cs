using Nicenis;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace GroupCalendar.Main.Converter
{
    /// <summary>
    /// Returns Visibility.Visible if all input values are falsy; otherwise, Visibility.Collapsed.
    /// </summary>
    /// <seealso cref="Booleany.IsTruthy"/>
    public class AllFalsyToVisibleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            return values.All(p => Booleany.IsFalsy(p)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Returns true if the input value is falsy; otherwise, false.
    /// </summary>
    /// <seealso cref="Booleany.IsTruthy"/>
    [ValueConversion(typeof(object), typeof(bool))]
    public class FalsyToTrueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Booleany.IsFalsy(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
