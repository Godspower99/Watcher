using System;
using System.Globalization;
using System.Windows.Media;

namespace Watcher_GUI
{
    public class TextToColorConverter : BaseValueConverter<TextToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)parameter)
            {
                // Master Diretion Convertion
                case "MD":
                    return (string)value == "up" ? new SolidColorBrush(Colors.LimeGreen) :
                           (string)value == "down" ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.LightGray);

                case "TD":
                    return (string)value == "up" ? new SolidColorBrush(Colors.LimeGreen) :
                         (string)value == "down" ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.LightGray);
                case "RE":
                    return (string)value == "up" ? new SolidColorBrush(Colors.LimeGreen) :
                         (string)value == "down" ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.LightGray);

                default:
                    return new SolidColorBrush(Colors.LightGray);
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
