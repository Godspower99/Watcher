using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.Azure.Devices.Provisioning;
using Microsoft.Azure.Devices.Provisioning.Service;

namespace Watcher_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private members

        /// <summary>
        /// watcher viewmodel expose'
        /// </summary>
        private WatcherViewModel watcherViewModel => App.GetService<WatcherViewModel>();

        #endregion

        #region Constructors
        /// <summary>
        /// default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            watchlist.ItemsSource = App.GetService<WatcherViewModel>().WatchList;
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// WatchList Event Handler for adding symbols to watchlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_watchlist_Click(object sender, RoutedEventArgs e)
        {
            new AddWatchlistDialog().ShowDialog();
        }

        /// <summary>
        /// Symbols event handler to open symbols dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Symbols_Click(object sender, RoutedEventArgs e)
        {
            App.GetService<SymbolsWindow>().ShowDialog();
        }

        /// <summary>
        /// Hub event handler to change IoTHub connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hub_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("COMING SOON!!");
            // Show login Dialog
           // bool? result = new WatcherLoginDialog().ShowDialog();
            // Load Main Window if login was successful
           // if (result == true)
           // {
                // Load Symbols for symbols and watchlist
          //      App.LoadSymbols();
           //     App.LoadWatchList();
           // }
        }

        /// <summary>
        /// Exit button event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion
    }
}
