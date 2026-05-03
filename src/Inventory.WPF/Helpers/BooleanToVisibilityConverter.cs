using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Inventory.WPF.Helpers
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        // bool -> Visibility
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible)
                return isVisible ? Visibility.Visible : Visibility.Collapsed;

            return Visibility.Collapsed;
        }

        // Visibility -> bool
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
                return visibility == Visibility.Visible;

            return false;
        }
    }
}