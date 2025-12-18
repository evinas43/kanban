using System;
using System.Globalization;
using System.Windows.Data;

namespace Kanban
{
    public class PrioritatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int p = 0;
            if (value is int)
                p = (int)value;

            switch (p)
            {
                case 1:
                    return "Low";
                case 2:
                    return "Mid";
                case 3:
                    return "High";
                default:
                    return "Unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            if (s == null) return 0;

            switch (s)
            {
                case "Low":
                    return 1;
                case "Mid":
                    return 2;
                case "High":
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
