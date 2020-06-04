using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Watcher_GUI
{
    /// <summary>
    /// Interaction logic for DatabaseSymbolsItem.xaml
    /// </summary>
    public partial class DatabaseSymbolsItem : UserControl
    {
        public DatabaseSymbolsItem()
        {
            InitializeComponent();
        }

        private void Control_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var symbol = (this.DataContext as Symbol).SymbolName;
            App.UpdateSymbolDescription(symbol);
        }
    }
}
