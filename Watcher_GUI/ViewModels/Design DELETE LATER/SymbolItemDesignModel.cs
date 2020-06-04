using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher_GUI
{
    public class SymbolItemDesignModel : Symbol
    {
        public static SymbolItemDesignModel SymbolDesignModel { get; set; } = new SymbolItemDesignModel {
            LongTermMasterDirection = "up",
            LongTermTrend = "down",
            LongTermRenkoBarType = "down",
            ShortTermMasterDirection = "down",
            ShortTermTrend = "up",
            ShortTermRenkoBarType = "up"
        };
        public SymbolItemDesignModel()
        {
     
        }
    }
}
