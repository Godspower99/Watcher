using System.Threading.Tasks;
using System.Windows.Controls;

namespace Watcher_GUI
{
    /// <summary>
    /// Interaction logic for AlertsListItem.xaml
    /// </summary>
    public partial class AlertsListItem : UserControl
    {
        public AlertsListItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Removes this item from Alerts List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.RemoveFromAlertsList((this.DataContext as AlertsViewModel).AlertID);
        }
    }
}
