namespace Wacton.Japangolin.UI.Converters;

using System;
using System.Globalization;
using System.Windows.Data;

public class BoolToInverseConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool)
        {
            return !(bool)value;
        }

        var type = value.GetType();
        throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}