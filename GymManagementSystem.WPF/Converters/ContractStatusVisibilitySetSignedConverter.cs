using GymManagementSystem.Core.Enum;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GymManagementSystem.WPF.Converters;

public class ContractStatusVisibilitySetSignedConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is ContractStatus status)
        {
            if(status == ContractStatus.Generated)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
