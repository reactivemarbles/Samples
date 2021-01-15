using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Imperative
{
    public abstract class ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}