using A9MTE_Stys.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace A9MTE_Stys.Converters
{
    public class CategoryEnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Enum.TryParse("Active", out CategoryEnum category);
            return category;
        }
    }
}
