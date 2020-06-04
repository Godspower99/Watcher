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
    /// Interaction logic for SymbolItem.xaml
    /// </summary>
    public partial class SymbolItem : UserControl
    {
        public SymbolItem()
        {
           InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // Stop Symbol Telemetry and Remove From Watchlist
            App.GetService<WatcherViewModel>().StopSymbolTelemetry(this.DataContext as Symbol).GetAwaiter();
            App.RemoveFromWatchList(symbolname.Text);
        }
    }
}
