using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Inventory.WPF.Helpers
{
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        // bool → Visibility (invertido)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isActive)
            {
                return isActive ? Visibility.Collapsed : Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        // No lo usamos pero WPF lo exige
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility != Visibility.Visible;
            }

            return false;
        }
    }
}