using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Comparison.Common;

namespace Imperative
{
    public class ThreadSafeViewModel : ViewModelBase, IThreadSafeViewModel
    {
        private string _firstName;
        private string _lastName;

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

        public string FullName => $"{FirstName} {LastName}";

        private void TriggerFullNameChanged() => OnPropertyChanged(nameof(FullName));
    }
}