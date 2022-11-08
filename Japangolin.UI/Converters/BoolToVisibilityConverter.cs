namespace Wacton.Japangolin.UI.Converters;

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

public class BoolToVisibilityConverter : IValueConverter
{
    public bool Hidden { get; set; }

    public bool Reverse { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }

        if (value is bool)
        {
            var isTrue = (bool)value;
            if (isTrue != Reverse)
            {
                return Visibility.Visible;
            }

            return Hidden ? Visibility.Hidden : Visibility.Collapsed;
        }

        var type = value.GetType();
        throw new InvalidOperationException("Unsupported type [" + type.Name + "]"); 
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}