using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;

namespace Watcher_GUI
{
    /// <summary>
    /// Interaction logic for AddWatchlistDialog.xaml
    /// </summary>
    public partial class AddWatchlistDialog : Window
    {
        public string IoTHubName => App.GetService<WatcherViewModel>().GetWatcher().IoTHubName;
        public AddWatchlistDialog()
        {
            InitializeComponent();
            this.DataContext = this;
            symbols.ItemsSource = App.GetService<WatcherViewModel>().Symbols;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            List<Symbol> failedSymbols = new List<Symbol>();

            // Add Selected Symbols to WatchList
            foreach (var item in App.SelectedSymbols)
            {
                // Skip if Symbol Already Exists in watchlist
                if (App.GetService<WatcherViewModel>().WatchList.ToList().FirstOrDefault(x => x.SymbolName == item.SymbolName) != null)
                    continue;
                
                // Test Symbol Connection
                var result = App.GetService<WatcherViewModel>().TestSymbolConnection(item).GetAwaiter().GetResult();
                if (result == true)
                {
                    App.GetService<WatcherViewModel>().WatchList.Add(item);
                    App.GetService<WatcherViewModel>().StartSymbolTelemetry(item).GetAwaiter();
                }

                if (result == false)
                    failedSymbols.Add(item);
            }

            // Reset SelectedSymbols
            App.SelectedSymbols = new List<Symbol>();
            
            // Update WatchList ItemSource
            App.GetService<MainWindow>().watchlist.ItemsSource = App.GetService<WatcherViewModel>().WatchList;

            // Print Failed Symbols
            if(failedSymbols?.Count > 0)
            {
                string message = "Symbols Failed To Connect\n";
                foreach (var item in failedSymbols)
                    message = message + "\n" + item.SymbolName;
                MessageBox.Show(message);
                failedSymbols = new List<Symbol>();
            }
          //  this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
