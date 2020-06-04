using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Watcher_GUI
{ 
    public class Symbol : BaseViewModel
    {
        /// <summary>
        /// The IotHub name this symbol is registered on
        /// </summary>
        public string IoTHubName { get; set; }

        [Key]
        /// <summary>
        /// Name of Symbol
        /// </summary>
        public string SymbolName { get; set; }

        /// <summary>
        /// Symbol Time chart device name
        /// </summary>
        public string TimeChartDeviceName { get; set; }

        /// <summary>
        /// Symbol long Renko Chart Device name
        /// </summary>
        public string LongRenkoChartDeviceName { get; set; }

        /// <summary>
        /// Symbol Short renko Chart name
        /// </summary>
        public string ShortRenkoChartDeviceName { get; set; }

        /// <summary>
        /// Symbol TimeChart Connection String
        /// </summary>
        public string TimeChartConnectionString { get; set; }

        /// <summary>
        /// Symbol Long Renko Chart Connection String
        /// </summary>
        public string LongRenkoChartConnectionString { get; set; }

        /// <summary>
        /// Symbol Short Renko Chart Connection String
        /// </summary>
        public string ShortRenkoChartConnectionString { get; set; }

        /// <summary>
        /// Long Term master direction from possibly D1
        /// </summary>
        public string LongTermMasterDirection { get; set; } = "";

        /// <summary>
        /// Short term master direction from possibly H4
        /// </summary>
        public string ShortTermMasterDirection { get; set; } = "";

        /// <summary>
        /// Long Term Trend from possibly H1
        /// </summary>
        public string LongTermTrend { get; set; } = "";

        /// <summary>
        /// Short term trend from possibly M30
        /// </summary>
        public string ShortTermTrend { get; set; } = "";

        /// <summary>
        /// Long Term ATR possibly D1
        /// </summary>
        public double LongTermATR { get; set; } = 0;

        /// <summary>
        /// Short term ATR possibly H4
        /// </summary>
        public double ShortTermATR { get; set; } = 0;

        /// <summary>
        /// Long Term Master Chart TimeFrame possibly D1
        /// </summary>
        public string LongTermMasterTimeFrame { get; set; } = "";

        /// <summary>
        /// Short Term Master Chart TimeFrame possibly H4
        /// </summary>
        public string ShortTermMasterTimeFrame { get; set; } = "";

        /// <summary>
        /// Long Term trend timframe possibly H1
        /// </summary>
        public string LongTermTrendTimeFrame { get; set; } = "";

        /// <summary>
        /// Short Term Trend TimeFrame possibly M30
        /// </summary>
        public string ShortTermTrendTimeFrame { get; set; } = "";

        /// <summary>
        /// Long Term Renko bar Type 
        /// </summary>
        public string LongTermRenkoBarType { get; set; } = "";

        /// <summary>
        /// Long Term Renko Bar Size
        /// </summary>
        public int LongTermRenkoBarSize { get; set; } = 0;

        /// <summary>
        /// Short Term Renko bar Type 
        /// </summary>
        public string ShortTermRenkoBarType { get; set; } = "";

        /// <summary>
        /// Short Term Renko Bar Size
        /// </summary>
        public int ShortTermRenkoBarSize { get; set; } = 0;

        /// <summary>
        /// Long Term Renko Last Update
        /// </summary>
        public string LongTermRenkoLastUpdate { get; set; } = DateTime.MinValue.ToLongTimeString();

        /// <summary>
        /// Short Term Renko Last Update
        /// </summary>
        public string ShortTermRenkoLastUpdate { get; set; } = DateTime.MinValue.ToLongTimeString();

        public void ToLower()
        {
            SymbolName = SymbolName.ToLower();
            TimeChartDeviceName = TimeChartDeviceName.ToLower();
            LongRenkoChartDeviceName = LongRenkoChartDeviceName.ToLower();
            ShortRenkoChartDeviceName = ShortRenkoChartDeviceName.ToLower();
        }

        /// <summary>
        /// Set time Chart Variables
        /// </summary>
        /// <param name="timeChartInformation"></param>
        /// <param name="chartType"></param>
        public void SetTimeChartInformation(TimeChartInformationDTO timeChartInformation, string chartType)
        {
            switch(chartType)
            {
                case "longtermtimechart":
                 {
                        LongTermMasterDirection = timeChartInformation.LongTermMasterDirection;
                        LongTermTrend = timeChartInformation.LongTermTrend;
                        LongTermMasterTimeFrame = timeChartInformation.LongTermMasterTimeFrame;
                        LongTermTrendTimeFrame = timeChartInformation.LongTermTrendTimeFrame;
                        LongTermATR = timeChartInformation.LongTermATR;
                        break;
                 }
                case "shorttermtimechart":
                    {
                        ShortTermMasterDirection = timeChartInformation.ShortTermMasterDirection;
                        ShortTermTrend = timeChartInformation.ShortTermTrend;
                        ShortTermMasterTimeFrame = timeChartInformation.ShortTermMasterTimeFrame;
                        ShortTermTrendTimeFrame = timeChartInformation.ShortTermTrendTimeFrame;
                        ShortTermATR = timeChartInformation.ShortTermATR;
                        break;
                    }
            }
        }

        /// <summary>
        /// Set Renko Chart Variables
        /// </summary>
        /// <param name="renkoChartInformation"></param>
        /// <param name="renkoMode"></param>
        public void SetRenkoChartInformation(RenkoChartInformationDTO renkoChartInformation, string renkoMode)
        {
            switch(renkoMode)
            {
                case "longterm":
                    {
                        LongTermRenkoBarType = renkoChartInformation.BarType;
                        LongTermRenkoBarSize = renkoChartInformation.BarSize;
                        LongTermRenkoLastUpdate = DateTime.Now.ToLongTimeString();
                        break;
                    }
                case "shortterm":
                    {
                        ShortTermRenkoBarType = renkoChartInformation.BarType;
                        ShortTermRenkoBarSize = renkoChartInformation.BarSize;
                        ShortTermRenkoLastUpdate = DateTime.Now.ToLongTimeString();
                        break;
                    }
            }
        }
    }
}
