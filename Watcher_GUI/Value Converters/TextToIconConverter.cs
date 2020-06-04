using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher_GUI
{
    public class TextToIconConverter : BaseValueConverter<TextToIconConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           switch((string)parameter)
            {
                // Master Diretion Convertion
                case "MD" :
                        return (string)value == "up" ? "\uf357" :
                               (string)value == "down" ? "\uf354" : "\uf128";

                case "TD" :
                        return (string)value == "up" ? "\uf201" :
                               (string)value == "down" ? "\uf201" : "\uf128";
                case "RE" :
                    return (string)value == "up" ? "\uf2fb" :
                            (string)value == "down" ? "\uf2fb" : "\uf128";

                default:
                        return "\uf128";
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
