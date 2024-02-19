using System.Globalization;
using System.Windows.Data;

namespace PL;

/// <summary>
/// class: convertIdToContent is to convert the button in addengineer to add or update
/// </summary>
public class ConvertIdToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Update";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

