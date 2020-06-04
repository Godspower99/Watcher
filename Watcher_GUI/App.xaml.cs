using InternetConnection;
using Microsoft.Azure.Devices.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Watcher_GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region private members

        /// <summary>
        /// Service Collection for DI
        /// </summary>
        private ServiceCollection serviceCollection;

        /// <summary>
        /// Service provider for DI
        /// </summary>
        private static ServiceProvider serviceProvider;

        /// <summary>
        /// Get required service from service provider
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>() => serviceProvider.GetRequiredService<T>();

        public static Watcher watcher;

        /// <summary>
        /// List of selected symbols to add to watchlist
        /// </summary>
        public static List<Symbol> SelectedSymbols { get; set; } = new List<Symbol>();

        #endregion

        /// <summary>
        /// Application Initialization Logic
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Allow default logic
            base.OnStartup(e);

            // Add Db Context to services
            serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<DatabaseContext>();

            // Add application viewmodel to services
            serviceCollection.AddSingleton<WatcherViewModel>();

            // Add MainWindow to Services
            serviceCollection.AddSingleton<MainWindow>();

            // Add SymbolsWindow to Services
            serviceCollection.AddSingleton<SymbolsWindow>();

            // build services
            serviceProvider = serviceCollection.BuildServiceProvider();

            // Set static Application Properties
            GetService<WatcherViewModel>().Context = serviceProvider.GetRequiredService<DatabaseContext>();

            // Ensure Database exists and has latest migrations
            GetService<WatcherViewModel>().Context.Database.Migrate();

            // Check for Internet Connection
            // Exit if no internet Connection
            if (new Connector().CheckIfInternetConnected() == false)
            {
                MessageBox.Show("Check your internet!!", "NO INTERNET ACCESS!!");
                this.Shutdown();
                return;
            }


            // TRY AUTO-LOGIN TO WATCHER
            // try to retrieve watcher from database
            bool watcherExists = GetService<WatcherViewModel>().HasConnectionParametersInStore();
            if (watcherExists)
            {
                // Try to Login To Event And Service Hub
                watcher = GetService<WatcherViewModel>().Context.Watcher.FirstOrDefault();
                var eventResult = GetService<WatcherViewModel>().LoginToEventHub(watcher).GetAwaiter().GetResult();
                var serviceResult = GetService<WatcherViewModel>().LoginToHubService(watcher).GetAwaiter().GetResult();

                // Load Main Window if login was successful
                if (eventResult.Item1 && serviceResult.Item1)
                {
                    // Load Symbols for symbols and watchlist
                    LoadSymbols();
                    LoadWatchList();
                    // Load main window
                    GetService<MainWindow>().Show();
                }

                // return error message on login failure 
                if (!eventResult.Item1 || !serviceResult.Item1)
                {
                    MessageBox.Show($"Cant Login with IoTHUB Settings in Store !!\n\n\t\tDETAILS\n\nEventHub > {eventResult.Item2} \nServiceHub > {serviceResult.Item2}", "LOGIN FAILED!!");
                    watcherExists = false;
                }
            }

            // LOGIN WATCHER MANUALLY IF NO DETAILS IS FOUND
            // OR AUTO LOGIN FAILED
            if (!watcherExists)
            {
                bool? result = new WatcherLoginDialog().ShowDialog();

                // Load Main Window if login was successful
                if (result == true)
                {
                    // Load Symbols for symbols and watchlist
                    LoadSymbols();
                    LoadWatchList();
                    // Load main window
                    GetService<MainWindow>().Show();
                }
                // Close App if cancel button was clicked in login dialog
                else if (result == false || result == null) { MessageBox.Show("Closing Application", "LOGIN CANCELLED"); this.Shutdown(); return; }
            }
        }

        /// <summary>
        /// Method to load symbols from watchlist
        /// </summary>
        public static void LoadSymbols()
        {
            // Load Symbols from database
            GetService<WatcherViewModel>().Symbols = new ObservableCollection<Symbol>(GetService<WatcherViewModel>().Context.Symbols.
                Where(x => x.IoTHubName == watcher.IoTHubName)?.ToList());
        }

        /// <summary>
        /// Load Watchlist From Symbols Registered to the watcher IoTHub
        /// </summary>
        public static void LoadWatchList()
        {
            // Load Symbols To WatchList
            GetService<WatcherViewModel>().WatchList = new ObservableCollection<Symbol>();
        }

        /// <summary>
        /// Removes a symbol from watchlist using its name
        /// </summary>
        /// <param name="symbol"></param>
        public static void RemoveFromWatchList(string symbol)
        {
            var symbolToDelete = GetService<WatcherViewModel>().WatchList.FirstOrDefault(x => x.SymbolName == symbol);
            GetService<WatcherViewModel>().WatchList.Remove(symbolToDelete);
            GetService<MainWindow>().watchlist.ItemsSource = GetService<WatcherViewModel>().WatchList;

            // TODO ::: STOP DEVICES FROM SENDING TELEMETRY
        }

        /// <summary>
        /// Update Symbol Description used in symbols window
        /// </summary>
        /// <param name="symbol"></param>
        public static void UpdateSymbolDescription(string symbol)
        {
            // Get symbol using symbol name
            var sym = GetService<WatcherViewModel>().Context.Symbols.FirstOrDefault(x => x.SymbolName == symbol);
            App.GetService<SymbolsWindow>().symbolname.Text = sym.SymbolName;
            App.GetService<SymbolsWindow>().timedevicename.Text = sym.TimeChartDeviceName;
            App.GetService<SymbolsWindow>().timeconnectionstring.Text = sym.TimeChartConnectionString;
            App.GetService<SymbolsWindow>().longrenkodevicename.Text = sym.LongRenkoChartDeviceName;
            App.GetService<SymbolsWindow>().longrenkoconnectionstring.Text = sym.LongRenkoChartConnectionString;
            App.GetService<SymbolsWindow>().shortrenkodevicename.Text = sym.ShortRenkoChartDeviceName;
            App.GetService<SymbolsWindow>().shortrenkoconnectionstring.Text = sym.ShortRenkoChartConnectionString;
        }

    }
}
