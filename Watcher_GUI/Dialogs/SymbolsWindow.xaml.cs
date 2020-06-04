using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Watcher_GUI
{
    /// <summary>
    /// Interaction logic for SymbolsWindow.xaml
    /// </summary>
    public partial class SymbolsWindow : Window
    {
        public SymbolsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            
            // Bind symbols itemsSource to WatcherViewModel.Symbols
            symbols.ItemsSource = App.GetService<WatcherViewModel>().Symbols;

            var sym = App.GetService<WatcherViewModel>().Context.Symbols.FirstOrDefault();
            symbolname.Text = sym?.SymbolName;
            timedevicename.Text = sym?.TimeChartDeviceName;
            timeconnectionstring.Text = sym?.TimeChartConnectionString;
            longrenkodevicename.Text = sym?.LongRenkoChartDeviceName;
            longrenkoconnectionstring.Text = sym?.LongRenkoChartConnectionString;
            shortrenkodevicename.Text = sym?.ShortRenkoChartDeviceName;
            shortrenkoconnectionstring.Text = sym?.ShortRenkoChartConnectionString;
        }

        /// <summary>
        /// Calls Add Symbol dialog to handle adding symbol to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new AddSymbolDialog().ShowDialog();
        }

        /// <summary>
        /// Closes This Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
