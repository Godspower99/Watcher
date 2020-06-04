using Microsoft.Azure.Devices;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Watcher_GUI
{
    public class WatcherViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Core Database 
        /// </summary>
        public DatabaseContext Context { get; set; }

        /// <summary>
        /// Watcher Event Hub Client
        /// </summary>
        public EventHubClient EventHubClient { get; set; }

        /// <summary>
        /// Watcher Event Hub Client
        /// </summary>
        public ServiceClient HubServiceClient { get; set; }

        /// <summary>
        /// Symbols Registered With Watcher
        /// </summary>
        public ObservableCollection<Symbol> WatchList { get; set; } = new ObservableCollection<Symbol>();

        /// <summary>
        /// Total Symbols registered in database
        /// </summary>
        public ObservableCollection<Symbol> Symbols { get; set; }

        /// <summary>
        /// Return object for dialog boxes
        /// </summary>
        public object DialogReturnObject { get; set; }

        /// <summary>
        /// Symbol description used in symbols window
        /// </summary>
        public Symbol SymbolDescription { get; set; }
        #endregion
    }
}
