using System.ComponentModel.DataAnnotations;

namespace Watcher_GUI
{
    public class Watcher
    {
        [Key]
        /// <summary>
        /// Key For Entiy Tracking
        /// </summary>
        public int id { get; set; }

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
    }
}
