using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace A9MTE_Stys.Converters
{
    public class TagsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = (List<string>)value;
            var tags = string.Empty;

            foreach (var tag in list)
            {
                tags += "#" + new string(tag.ToCharArray()
                                            .Where(c => !Char.IsWhiteSpace(c))
                                            .ToArray()) + " "; 
            }

            return tags;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
