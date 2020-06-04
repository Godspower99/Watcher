using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Watcher_GUI
{
    /// <summary>
    /// A BaseViewModel Implementing the INotifyPropertyChanged Interface
    /// for Notifying property value changes
    /// </summary>
    [ImplementPropertyChanged]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string PropertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
