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
            LongTermTrend = "up",
            LongTermRenkoBarType = "up",
            ShortTermMasterDirection = "up",
            ShortTermTrend = "up",
            ShortTermRenkoBarType = "up"
        };
        public SymbolItemDesignModel()
        {
     
        }
    }
}
