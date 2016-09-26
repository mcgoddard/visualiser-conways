using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace visualiser_conways.Helpers
{
    class BoolToColorConverter : IValueConverter
    {
        private static SolidColorBrush deadBrush = new SolidColorBrush(Colors.Black);
        private static SolidColorBrush aliveBrush = new SolidColorBrush(Colors.AliceBlue);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(bool))
            {
                if ((bool)value)
                {
                    return aliveBrush;
                }
                else
                {
                    return deadBrush;
                }
            }
            else
            {
                throw new ArgumentException("This converter can only accept boolean values");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
