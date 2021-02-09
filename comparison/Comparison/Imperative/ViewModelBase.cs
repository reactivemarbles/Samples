using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Imperative
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private readonly SynchronizationContext _mainThread = SynchronizationContext.Current;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _mainThread.Send(_ =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }, null);
        }

        protected void SetPropertySafe<T>(T currentValue, T newValue, Action<T> setValue, [CallerMemberName] string property = null)
        {
            _mainThread.Send(callBack =>
            {
                var obj = (T) callBack;

                if (!EqualityComparer<T>.Default.Equals(obj, currentValue))
                {
                    setValue(obj);
                    OnPropertyChanged(property);
                }
            }, newValue);
        }
    }
}