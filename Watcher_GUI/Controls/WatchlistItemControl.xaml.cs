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
    /// Interaction logic for WatchlistItemControl.xaml
    /// </summary>
    public partial class WatchlistItemControl : UserControl
    {
        public WatchlistItemControl()
        {
            InitializeComponent();
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            // Add this Symbol to the SelectedSymbols
            App.SelectedSymbols.Add(this.DataContext as Symbol);
        }

        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Remove this Symbol From Selected Symbols
            App.SelectedSymbols.Remove(App.SelectedSymbols.FirstOrDefault(x => x.SymbolName == (this.DataContext as Symbol).SymbolName));
        }
    }
}
