using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BurgerMenu_Desktop.Helpers;

public class NumericFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string input)
        {
            if (long.TryParse(input, out long number))
            {
                CultureInfo uzbekCulture = new CultureInfo("uz-Cyrl-UZ");
                return number.ToString("C0", uzbekCulture).Replace("сўм", "сўм ");
            }
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
       throw new NotSupportedException();
    }
}
