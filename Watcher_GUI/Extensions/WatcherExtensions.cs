using Microsoft.Azure.Devices;
using Microsoft.Azure.EventHubs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Watcher_GUI
{
    public static class WatcherExtensions
    {
        /// <summary>
        /// Retrieve Watcher Stored Data From Database
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static Watcher GetWatcher(this DatabaseContext db)
        {
            var watcher =  db.Watcher.FirstOrDefault();
            return watcher != null ? watcher : null;
        }

        /// <summary>
        /// Store Watcher Data
        /// </summary>
        /// <param name="db"></param>
        /// <param name="watcher"></param>
        /// <returns></returns>
        public static Watcher SetWatcher(this DatabaseContext db, Watcher watcher)
        {
            db.Watcher.Add(watcher);
            db.SaveChanges();
            return watcher;
        }

        /// <summary>
        /// Returns true if watcher connection parameters exist in database
        /// </summary>
        /// <param name="core"></param>
        /// <returns></returns>
        public static bool HasConnectionParametersInStore(this WatcherViewModel core)
        {
            var watcher = core.Context.Watcher.FirstOrDefault();
            return watcher != null ? true : false;
        }

        /// <summary>
        /// Returns Watcher parameters stored in database
        /// </summary>
        /// <param name="core"></param>
        /// <returns></returns>
        public static Watcher GetWatcher(this WatcherViewModel core)
        {
            return core.Context.Watcher.FirstOrDefault();
        }

        /// <summary>
        /// login Watcher to event HUB
        /// With Watcher as Parameter
        /// </summary>
        /// <param name="core"></param>
        /// <param name="watcher"></param>
        /// <returns></returns>
        public static async Task<Tuple<bool, string>> LoginToEventHub(this WatcherViewModel core, Watcher watcher)
        {
            try
            {
                // connect watcher to EventHub
                var connectionString = new EventHubsConnectionStringBuilder(new Uri(watcher.EventHubEndpoint), watcher.EventHubPath, watcher.EventHubKeyName, watcher.EventHubPrimayKey).ToString();
                core.EventHubClient = EventHubClient.CreateFromConnectionString(connectionString);

                // Create a PartitionReciever for each partition on the hub.
                var runtimeInfo = core.EventHubClient?.GetRuntimeInformationAsync().GetAwaiter().GetResult();
                var d2cPartitions = runtimeInfo.PartitionIds;

                // Create Partition Listener Tasks
                var tasks = new List<Task>();
                foreach (string partition in d2cPartitions)
                {
                    tasks.Add(core.PartitionListener(partition));
                }

                // Run partition receivers
                Task.WhenAll(tasks);

                return Tuple.Create<bool, string>(true, "success");
            }
            catch (Exception ex)
            {
                // return failed on error
                return Tuple.Create<bool, string>(false, ex.Message);
            }
        }

        /// <summary>
        /// login Watcjer to service hub
        /// </summary>
        /// <param name="core"></param>
        /// <param name="watcher"></param>
        /// <returns></returns>
        public static async Task<Tuple<bool,string>> LoginToHubService(this WatcherViewModel core, Watcher watcher)
        {
            try
            {
                var connectionString = $"HostName={watcher.IoTHubName}.azure-devices.net;SharedAccessKeyName={watcher.EventHubKeyName};SharedAccessKey={watcher.EventHubPrimayKey}";

                // Create a ServiceClient to communicate with service-facing endpoint on IoTHub.
                core.HubServiceClient = ServiceClient.CreateFromConnectionString(connectionString);
                core.HubServiceClient.GetServiceStatisticsAsync().GetAwaiter().GetResult();
                return Tuple.Create<bool,string>(true, "success");
            }
            catch (Exception ex)
            {
                // return failed on error
                return Tuple.Create<bool, string>(false, ex.Message);
            }
        }

        /// <summary>
        /// Test Chart Connections 
        /// </summary>
        /// <param name="core"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static async Task<bool> TestSymbolConnection(this WatcherViewModel core, Symbol symbol)
        {
            try
            {
                // command to send to charts
                var command = JsonConvert.SerializeObject(new WatcherToSymbolCommandDTO { Command = "testconnection" });

                // Method to invoke on device
                var methodInvocation = new CloudToDeviceMethod("WatcherToSymbolCommand") { ResponseTimeout = TimeSpan.FromSeconds(10) };
                methodInvocation.SetPayloadJson(command);

                // Invoke Method on TimeChart
                var timeResponse = core.HubServiceClient.InvokeDeviceMethodAsync(symbol.TimeChartDeviceName, methodInvocation).GetAwaiter().GetResult();
                // Invoke Method on Short Renko Chart
                var shortRenkoResponse = core.HubServiceClient.InvokeDeviceMethodAsync(symbol.ShortRenkoChartDeviceName, methodInvocation).GetAwaiter().GetResult();

                // Invoke Method on Long Renko Chart
                var longRenkoResponse = core.HubServiceClient.InvokeDeviceMethodAsync(symbol.LongRenkoChartDeviceName, methodInvocation).GetAwaiter().GetResult();

                // Convert Response to dotNet Object
                var timeResponseObject = JsonConvert.DeserializeObject<WatcherResponse>(timeResponse.GetPayloadAsJson());
                var shortRenkoResponseObject = JsonConvert.DeserializeObject<WatcherResponse>(shortRenkoResponse.GetPayloadAsJson());
                var longRenkoResponseObject = JsonConvert.DeserializeObject<WatcherResponse>(longRenkoResponse.GetPayloadAsJson());

                if (timeResponseObject.response == "connected" &&
                   shortRenkoResponseObject.response == "connected" &&
                   longRenkoResponseObject.response == "connected")
                    return true;
                return false;
            }
            catch{ return false; }
        }

        /// <summary>
        /// Start Telemetry on Symbol Charts
        /// </summary>
        /// <param name="core"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static async Task StartSymbolTelemetry(this WatcherViewModel core, Symbol symbol)
        {
            try
            {
                 // command to send to charts
                var command = JsonConvert.SerializeObject(new WatcherToSymbolCommandDTO { Command = "starttelemetry" });

                // Method to invoke on device
                var methodInvocation = new CloudToDeviceMethod("WatcherToSymbolCommand") { ResponseTimeout = TimeSpan.FromSeconds(10) };
                methodInvocation.SetPayloadJson(command);

                // Invoke Method on TimeChart
                var timeResponse = core.HubServiceClient.InvokeDeviceMethodAsync(symbol.TimeChartDeviceName, methodInvocation).GetAwaiter().GetResult();
                // Invoke Method on Short Renko Chart
                var shortRenkoResponse = core.HubServiceClient.InvokeDeviceMethodAsync(symbol.ShortRenkoChartDeviceName, methodInvocation).GetAwaiter().GetResult();
                // Invoke Method on Long Renko Chart
                var longRenkoResponse = core.HubServiceClient.InvokeDeviceMethodAsync(symbol.LongRenkoChartDeviceName, methodInvocation).GetAwaiter().GetResult();

                // Convert Response to dotNet Object
                var timeResponseObject = JsonConvert.DeserializeObject<WatcherResponse>(timeResponse.GetPayloadAsJson());
                var shortRenkoResponseObject = JsonConvert.DeserializeObject<WatcherResponse>(shortRenkoResponse.GetPayloadAsJson());
                var longRenkoResponseObject = JsonConvert.DeserializeObject<WatcherResponse>(longRenkoResponse.GetPayloadAsJson());
            }

            catch { }
        }

        /// <summary>
        /// Stop Telemetry on Symbol Charts
        /// </summary>
        /// <param name="core"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static async Task StopSymbolTelemetry(this WatcherViewModel core, Symbol symbol)
        {
            try
            {
                // command to send to charts
                var command = JsonConvert.SerializeObject(new WatcherToSymbolCommandDTO { Command = "stoptelemetry" });

                // Method to invoke on device
                var methodInvocation = new CloudToDeviceMethod("WatcherToSymbolCommand") { ResponseTimeout = TimeSpan.FromSeconds(10) };
                methodInvocation.SetPayloadJson(command);

                // Invoke Method on TimeChart
                var timeResponse = core.HubServiceClient.InvokeDeviceMethodAsync(symbol.TimeChartDeviceName, methodInvocation).GetAwaiter().GetResult();
                // Invoke Method on Short Renko Chart
                var shortRenkoResponse = core.HubServiceClient.InvokeDeviceMethodAsync(symbol.ShortRenkoChartDeviceName, methodInvocation).GetAwaiter().GetResult();
                // Invoke Method on Long Renko Chart
                var longRenkoResponse = core.HubServiceClient.InvokeDeviceMethodAsync(symbol.LongRenkoChartDeviceName, methodInvocation).GetAwaiter().GetResult();

                // Convert Response to dotNet Object
                var timeResponseObject = JsonConvert.DeserializeObject<WatcherResponse>(timeResponse.GetPayloadAsJson());
                var shortRenkoResponseObject = JsonConvert.DeserializeObject<WatcherResponse>(shortRenkoResponse.GetPayloadAsJson());
                var longRenkoResponseObject = JsonConvert.DeserializeObject<WatcherResponse>(longRenkoResponse.GetPayloadAsJson());
            }

            catch { }
        }

        /// <summary>
        /// Add symbol to WatchList
        /// </summary>
        public static void AddToWatchList(this WatcherViewModel core, Symbol symbol)
        {
            core.WatchList.Add(symbol);
        }

        /// <summary>
        /// Update Watchlist With All Symbols in database
        /// </summary>
        /// <param name="core"></param>
        /// <returns></returns>
        public static async Task GetSymbols(this WatcherViewModel core)
        {
           core.Symbols = new ObservableCollection<Symbol>(await core.Context.Symbols.ToListAsync());
        }

        /// <summary>
        /// Add Symbols 
        /// </summary>
        /// <param name="core"></param>
        /// <param name="symbol"></param>
        public static void AddToSymbols(this WatcherViewModel core, Symbol symbol)
        {
            core.Context.Symbols.Add(symbol);
            core.Context.SaveChanges();
        }

        /// <summary>
        /// Receive and Process Data from EventHub
        /// </summary>
        /// <param name="core"></param>
        /// <param name="partition"></param>
        /// <returns></returns>
        private static async Task PartitionListener(this WatcherViewModel core, string partition)
        {
            // Create the receiver using the default consumer group.
            var eventHubReceiver = core.EventHubClient.CreateReceiver("$Default", partition, EventPosition.FromEnqueuedTime(DateTime.Now));
            while (true)
            {
                // Check for EventData - this method times out if there is nothing to retrieve.
                var events = await eventHubReceiver.ReceiveAsync(100);

                // If there is data in the batch, process it.
                if (events == null) continue;

                //TODO: Process Data received Here
                foreach (var data in events)
                    core.ProcessTelemetry(data);
            }
        }

        /// <summary>
        /// Add Or Update Watcher to database
        /// </summary>
        /// <param name="core"></param>
        /// <param name="watcher"></param>
        /// <returns></returns>
        public static Watcher RegisterWatcher(this WatcherViewModel core, Watcher watcher)
        {
            // Retrive number of watchers in database
            var count =  core.Context.Watcher.Count();

            // replace current watcher
            if (count > 0)
            {

               var watcherInDB = core.Context.Watcher.Find(
                   core.Context.Watcher.FirstOrDefault().id);

                watcherInDB.EventHubEndpoint = watcher.EventHubEndpoint;
                watcherInDB.IoTHubName = watcher.IoTHubName;
                watcherInDB.EventHubKeyName = watcher.EventHubKeyName;
                watcherInDB.EventHubPath = watcher.EventHubPath;
                watcherInDB.EventHubPrimayKey = watcher.EventHubPrimayKey;
                core.Context.SaveChanges();
            }
            core.Context.SetWatcher(watcher);
            return watcher;
        }

        /// <summary>
        /// This Method processes the telemetry received from the iot hub
        /// </summary>
        /// <param name="data"></param>
        private static async void ProcessTelemetry(this WatcherViewModel core, EventData data)
        {
            // Make Sure Watchlist is not empty to continue
            if (App.GetService<WatcherViewModel>().WatchList.Count == 0)
                return;

            // read properties from event data
            object chartprop, symbolprop;
            data.Properties.TryGetValue("symbolname",out symbolprop);
            data.Properties.TryGetValue("chart", out chartprop);
            string chartType = (chartprop as string).ToLower();
            string symbolName = (symbolprop as string).ToLower();

            // Fetch symbol from watchlist if it exists
            var symbol = App.GetService<WatcherViewModel>().WatchList.FirstOrDefault(x => x.SymbolName.Trim().ToLower() == symbolName.Trim().ToLower());

            // Long Term Time Chart
            if(chartType == "longtermtimechart")
            {
                // convert telemetry to TimeChartInformation Object
                var timeChartInformation = JsonConvert.DeserializeObject<TimeChartInformationDTO>(Encoding.UTF8.GetString(data.Body.Array));
                //update symbol values 
                symbol.SetTimeChartInformation(timeChartInformation, "longtermtimechart");
            }
            
            // Short Term Time Chart
           else if (chartType == "shorttermtimechart")
           {
                // convert telemetry to TimeChartInformation Object
                var timeChartInformation = JsonConvert.DeserializeObject<TimeChartInformationDTO>(Encoding.UTF8.GetString(data.Body.Array));
                //update symbol values 
                symbol.SetTimeChartInformation(timeChartInformation, "shorttermtimechart");
           }
            
            // Renko Charts
           else if(chartType == "renko")
            {
                // read Renko Specific Properties from event data
                object renkoModeProp, triggerProp;
                data.Properties.TryGetValue("renkomode", out renkoModeProp);
                data.Properties.TryGetValue("trigger", out triggerProp);
                string renkoMode = (renkoModeProp as string).ToLower();
                string trigger = (triggerProp as string).ToLower(); ;

                // Long Term Renko Charts
               if(renkoMode == "longterm")
                {
                    // convert telemetry to RenkoChartInformation Object
                    var renkoChartInformation = JsonConvert.DeserializeObject<RenkoChartInformationDTO>(Encoding.UTF8.GetString(data.Body.Array));
                    symbol.SetRenkoChartInformation(renkoChartInformation, "longterm");

                    // TODO :: CHECK FOR TRIGGER AND CALL TRIGGER ANALYZER TO ALERT
                }

               else if(renkoMode == "shortterm")
                {
                    // convert telemetry to RenkoChartInformation Object
                    var renkoChartInformation = JsonConvert.DeserializeObject<RenkoChartInformationDTO>(Encoding.UTF8.GetString(data.Body.Array));
                    symbol.SetRenkoChartInformation(renkoChartInformation, "shortterm");

                    // TODO :: CHECK FOR TRIGGER AND CALL TRIGGER ANALYZER TO ALERT
                }
            }            
        }
    }
}
