using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher_GUI
{
   public class MainWindowViewModel
   {
        public WatcherViewModel Watcher { get; set; }
        public MainWindowViewModel(WatcherViewModel watcher)
        {
            Watcher = watcher;
        }
   }
}
