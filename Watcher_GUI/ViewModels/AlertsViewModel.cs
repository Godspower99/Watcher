using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher_GUI
{
    /// <summary>
    /// Alert Type Enumeration for different types of alerts
    /// </summary>
    public enum AlertType
    {
        description,
        entrytrigger,
    }

    public class AlertsViewModel : BaseViewModel
    {
        /// <summary>
        /// Alert ID for Tracking in Alerts list
        /// </summary>
        public string AlertID { get; }

        /// <summary>
        /// Name of symbol raising alert
        /// </summary>
        public string SymbolName { get; set; }

        public string AlertTime { get; }

        /// <summary>
        /// The Title of the Alert
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Type of alert to raise
        /// </summary>
        public AlertType AlertType { get; set; } = AlertType.description;

        /// <summary>
        /// Default Constructor 
        /// </summary>
        public AlertsViewModel()
        {
            // Set alert time to time of creation
            AlertTime = DateTime.Now.ToLongTimeString();
            AlertID = new Guid().ToString();
        }
    }
}
