using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Comparison.Common;
using ReactiveUI;

namespace Declarative
{
    public class ThreadSafeViewModel : ReactiveObject, IThreadSafeViewModel
    {
        private string _firstName;
        private string _lastName;
        private readonly ObservableAsPropertyHelper<string> _fullName;

        public ThreadSafeViewModel()
        {
            this.WhenAnyValue(x => x.FirstName,
                    x => x.LastName,
                    (first, last) => $"{first} {last}")
                .ToProperty(this, nameof(FullName), out _fullName, scheduler: RxApp.MainThreadScheduler);
        }

        public string FirstName
        {
            get => _firstName;
            set => RxApp.MainThreadScheduler.Schedule(() => this.RaiseAndSetIfChanged(ref _firstName, value));
        }

        public string LastName
        {
            get => _lastName;
            set => RxApp.MainThreadScheduler.Schedule(() => this.RaiseAndSetIfChanged(ref _lastName, value));
        }

        public string FullName => _fullName.Value;
    }
}