using System.Windows.Data;

namespace WpfDatagridGrouping;

[ValueConversion(typeof(Boolean), typeof(String))]
public class CompleteConverter : IValueConverter
{
    // This converter changes the value of a Tasks Complete status from true/false to a string value of
    // "Complete"/"Active" for use in the row group header.
    public object Convert(object? value, Type targetType, object? parameter,
        System.Globalization.CultureInfo culture)
    {
        if (value is true)
            return "Complete";
        return "Active";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter,
        System.Globalization.CultureInfo culture)
    {
        return value as string == "Complete";
    }
}