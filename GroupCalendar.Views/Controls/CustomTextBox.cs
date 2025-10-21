using System.Windows;
using HandyControl.Controls;

namespace GroupCalendar.Views.Controls
{
    public class CustomTextBox : TextBox
    {

        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register
        (
            name: nameof(PlaceholderText),
            propertyType: typeof(string),
            ownerType: typeof(CustomTextBox),
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

        public static readonly DependencyProperty IsErrorHiddenProperty = DependencyProperty.Register
        (
            name: nameof(IsErrorHidden),
            propertyType: typeof(bool),
            ownerType: typeof(CustomTextBox),
            typeMetadata: new FrameworkPropertyMetadata(true)
        );

        /// <summary>
        /// 값이 없을때 표시할 문자열
        /// </summary>
        public bool IsErrorHidden
        {
            get => (bool)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }
    }
}
