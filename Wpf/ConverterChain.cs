using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Jsc.Wpf
{
    [ContentProperty("Converters")]
    [ContentWrapper(typeof(ConverterCollection))]
    public class ConverterChain : IValueConverter
    {
        public ICollection<IValueConverter> Converters { get; set; } = new Collection<IValueConverter>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Converters
                .Aggregate(value, (val, converter) => converter.Convert(val, targetType, parameter, culture));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Converters
                .Reverse()
                .Aggregate(value, (val, converter) => converter.ConvertBack(val, targetType, parameter, culture));
        }
    }

    public class ConverterCollection : Collection<IValueConverter> { }    
}
