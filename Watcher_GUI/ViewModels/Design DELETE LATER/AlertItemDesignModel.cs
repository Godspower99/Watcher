using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher_GUI
{
    public class AlertItemDesignModel : AlertsViewModel
    {
        public static AlertItemDesignModel DesignModel { get; set; } = new AlertItemDesignModel {
           SymbolName = "EURUSD",
           Title = "TESTING",
        };
        public AlertItemDesignModel()
        {

        }
    }
}
