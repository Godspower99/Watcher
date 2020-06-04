using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher_GUI
{
    public class TimeChartInformationDTO
    {
        [Key]
        /// <summary>
        /// Chart Symbol Name
        /// </summary>
        public string SymbolName { get; set; }

        /// <summary>
        /// Long Term master direction from possibly D1
        /// </summary>
        public string LongTermMasterDirection { get; set; }

        /// <summary>
        /// Short term master direction from possibly H4
        /// </summary>
        public string ShortTermMasterDirection { get; set; }

        /// <summary>
        /// Long Term Trend from possibly H1
        /// </summary>
        public string LongTermTrend { get; set; }

        /// <summary>
        /// Short term trend from possibly M30
        /// </summary>
        public string ShortTermTrend { get; set; }

        /// <summary>
        /// Long Term ATR possibly D1
        /// </summary>
        public double LongTermATR { get; set; }

        /// <summary>
        /// Short term ATR possibly H4
        /// </summary>
        public double ShortTermATR { get; set; }

        /// <summary>
        /// Long Term Master Chart TimeFrame possibly D1
        /// </summary>
        public string LongTermMasterTimeFrame { get; set; }

        /// <summary>
        /// Short Term Master Chart TimeFrame possibly H4
        /// </summary>
        public string ShortTermMasterTimeFrame { get; set; }

        /// <summary>
        /// Long Term trend timframe possibly H1
        /// </summary>
        public string LongTermTrendTimeFrame { get; set; }

        /// <summary>
        /// Short Term Trend TimeFrame possibly M30
        /// </summary>
        public string ShortTermTrendTimeFrame { get; set; }
    }
}
