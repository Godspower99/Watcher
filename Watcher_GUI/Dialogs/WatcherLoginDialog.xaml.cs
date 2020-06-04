using System.Windows;

namespace Watcher_GUI
{
    /// <summary>
    /// Interaction logic for WatcherLoginDialog.xaml
    /// </summary>
    public partial class WatcherLoginDialog : Window
    {
        #region Watcher Properties

        /// <summary>
        /// Watcher IoTHub name 
        /// </summary>
        public string IoTHubName { get; set; }

        /// <summary>
        /// Watcher Event Hub Endpoint
        /// </summary>
        public string EventHubEndpoint { get; set; }

        /// <summary>
        /// Watcher Event Hub Path
        /// </summary>
        public string EventHubPath { get; set; }

        /// <summary>
        /// Watcher Event Hub Key Name
        /// </summary>
        public string EventHubKeyName { get; set; }

        /// <summary>
        /// Watche Event Hub Connection String
        /// </summary>
        public string EventHubPrimayKey { get; set; }

        #endregion

        public WatcherLoginDialog()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Watcher Manual Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Terminate if any parameter is empty
            if(!AllParametersEntered())
            {
                MessageBox.Show("Enter all parameters","ERROR!!");
                return;
            }
            // Create Watcher DATA
            App.watcher = new Watcher
            {
                 IoTHubName = this.IoTHubName,
                 EventHubEndpoint = this.EventHubEndpoint,
                 EventHubKeyName = this.EventHubKeyName,
                 EventHubPath = this.EventHubPath,
                 EventHubPrimayKey = this.EventHubPrimayKey,
            };

            // Try to Login To Event And Service Hub
            var eventResult = App.GetService<WatcherViewModel>().LoginToEventHub(App.watcher).GetAwaiter().GetResult();
            var serviceResult = App.GetService<WatcherViewModel>().LoginToHubService(App.watcher).GetAwaiter().GetResult();

            // close this window if login was successful
            if (eventResult.Item1 && serviceResult.Item1)
            {
                // Add watcher settings to database
                App.GetService<WatcherViewModel>().RegisterWatcher(App.watcher);
                this.DialogResult = true;
                this.Close();
                return;
             }

            // return error message when login fails
            MessageBox.Show($"Cant Login with These IoTHUB Parameters !!\n\n\t\tDETAILS\n\nEventHub > {eventResult.Item2} \nServiceHub > {serviceResult.Item2}", "LOGIN FAILED!!");
            ResetParameters();
        }

        /// <summary>
        /// Cancel Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
            return;
        }

        /// <summary>
        /// Checks if all parameters has been entered
        /// </summary>
        /// <returns></returns>
        private bool AllParametersEntered()
        {
            return (string.IsNullOrWhiteSpace(IoTHubName) ||
                string.IsNullOrWhiteSpace(EventHubEndpoint) ||
                string.IsNullOrWhiteSpace(EventHubPath) ||
                string.IsNullOrWhiteSpace(EventHubKeyName) ||
                string.IsNullOrWhiteSpace(EventHubPrimayKey)) ? false : true;
                   
        }

        /// <summary>
        /// Reset all parameters
        /// </summary>
        private void ResetParameters()
        {
            IoTHubName = "";
            EventHubEndpoint = "";
            EventHubPath = "";
            EventHubKeyName = "";
            EventHubPrimayKey = "";
            hubname.Text = "";
            endpoint.Text = "";
            path.Text = "";
            keyname.Text = "";
            key.Text = "";
        }
    }
}
