using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Jsc.Wpf
{
    public class BoolToVisabilityConverter : ConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var falseVisibility = Visibility.Collapsed;

            if (parameter != null && parameter.GetType() == typeof(Visibility)) falseVisibility = (Visibility)parameter;

            return (bool)value ? Visibility.Visible : falseVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible;
        }
    }
}
