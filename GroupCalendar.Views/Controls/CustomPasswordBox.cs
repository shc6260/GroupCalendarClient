using System.Windows;
using HandyControl.Controls;

namespace GroupCalendar.Views.Controls
{
    public class CustomPasswordBox : PasswordBox
    {
        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register
       (
           name: nameof(PlaceholderText),
           propertyType: typeof(string),
           ownerType: typeof(CustomPasswordBox),
           typeMetadata: new FrameworkPropertyMetadata(null)
       );

        /// <summary>
        /// 값이 없을때 표시할 문자열
        /// </summary>
        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }
    }
}
