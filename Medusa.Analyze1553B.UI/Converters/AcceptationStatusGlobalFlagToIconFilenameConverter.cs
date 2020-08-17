using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using static Medusa.Analyze1553B.VM.IPageViewModel;

namespace Medusa.Analyze1553B.UI.Converters
{
    [ValueConversion(typeof(States), typeof(string))]
    public class AcceptationStatusGlobalFlagToIconFilenameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((States)value)
            {
                case States.Red:
                    return "Icons/redCircle.png";
                case States.Yellow:
                    return "Icons/yellowCircle.png";
                case States.Green:
                    return "Icons/greenCircle.png";
                default:
                    return null;
            }

            // or
            //return Enum.GetName(typeof(States), value) + ".jpg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
