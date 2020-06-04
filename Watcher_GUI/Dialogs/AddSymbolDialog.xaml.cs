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
using System.Windows.Shapes;

namespace Watcher_GUI
{
    /// <summary>
    /// Interaction logic for AddSymbolDialog.xaml
    /// </summary>
    public partial class AddSymbolDialog : Window
    {
        #region Pulic Properties
        public string SymbolName { get; set; }
        public string TimeChartDeviceName { get; set; }
        public string TimeChartConnectionString { get; set; }
        public string LongRenkoChartDeviceName { get; set; }
        public string LongRenkoChartConnectionString { get; set; }
        public string ShortRenkoChartDeviceName { get; set; }
        public string ShortRenkoChartConnectionString { get; set; }

        private Symbol symbol;
        #endregion
        public AddSymbolDialog()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // check if all parameters have been filled
            if (!AllParametersEntered())
            {
                MessageBox.Show("ENTER ALL PARAMETERS");
                return;
            }

            // symbol data to store in database
            symbol = new Symbol()
            {
                IoTHubName = App.watcher.IoTHubName,
                TimeChartDeviceName = this.TimeChartDeviceName,
                TimeChartConnectionString = this.TimeChartConnectionString,
                LongRenkoChartDeviceName = this.LongRenkoChartDeviceName,
                LongRenkoChartConnectionString = this.LongRenkoChartConnectionString,
                ShortRenkoChartDeviceName = this.ShortRenkoChartDeviceName,
                ShortRenkoChartConnectionString = this.ShortRenkoChartConnectionString,
                SymbolName = this.SymbolName
            };
            // Add Symbol to database
            App.GetService<WatcherViewModel>().AddToSymbols(symbol);

            // Update Symbols List
            App.LoadSymbols();
            App.GetService<SymbolsWindow>().symbols.ItemsSource = App.GetService<WatcherViewModel>().Symbols;
            this.Close();
        }

        /// <summary>
        /// Closes this dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            symbol = new Symbol()
            {
                IoTHubName = App.watcher.IoTHubName,
                TimeChartDeviceName = this.TimeChartDeviceName,
                TimeChartConnectionString = this.TimeChartConnectionString,
                LongRenkoChartDeviceName = this.LongRenkoChartDeviceName,
                LongRenkoChartConnectionString = this.LongRenkoChartConnectionString,
                ShortRenkoChartDeviceName = this.ShortRenkoChartDeviceName,
                ShortRenkoChartConnectionString = this.ShortRenkoChartConnectionString,
                SymbolName = this.SymbolName
            };

            var result = App.GetService<WatcherViewModel>().TestSymbolConnection(symbol).GetAwaiter().GetResult();

            if (result)
            { MessageBox.Show("Connected!"); return; }
            MessageBox.Show("Not Connected!!");
        }

        /// <summary>
        /// returns true if all parameters have been filled
        /// </summary>
        /// <returns></returns>
        private bool AllParametersEntered()
        {
            return (string.IsNullOrWhiteSpace(SymbolName) ||
               string.IsNullOrWhiteSpace(TimeChartDeviceName) ||
               string.IsNullOrWhiteSpace(TimeChartConnectionString) ||
               string.IsNullOrWhiteSpace(LongRenkoChartDeviceName) ||
               string.IsNullOrWhiteSpace(LongRenkoChartConnectionString) ||
               string.IsNullOrWhiteSpace(ShortRenkoChartDeviceName) ||
               string.IsNullOrWhiteSpace(ShortRenkoChartConnectionString)) ? false : true;
        }
    }
}
