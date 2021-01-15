using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Imperative
{
    public class ThreadSafeViewModel : ViewModelBase
    {
        private readonly SynchronizationContext _mainThread;
        private string _firstName;
        private string _lastName;

        public ThreadSafeViewModel()
        {
            _mainThread = SynchronizationContext.Current;
            var thread = new Thread(() => {
                for (int i = 0; i < 10; i++)
                {
                    FirstName = FirstName + i;
                    Thread.Sleep(500);
                    LastName = LastName + i;
                }
            });
            
            thread.Start();
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                SetPropertySafe(_firstName, value, current => _firstName = current);
                TriggerFullNameChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                SetPropertySafe(_lastName, value, current => _lastName = current);
                TriggerFullNameChanged();
            }
        }

        public string FullName => FirstName + LastName;

        private void SetPropertySafe<T>(T currentValue, T newValue, Action<T> setValue, [CallerMemberName] string property = null)
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

        private void TriggerFullNameChanged() => _mainThread.Send(callBack => { OnPropertyChanged(nameof(FullName)); }, null);
    }
}