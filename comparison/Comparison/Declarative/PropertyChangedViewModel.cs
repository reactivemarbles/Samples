using ReactiveUI;

namespace Declarative
{
    public class PropertyChangedViewModel : ReactiveObject
    {
        private string _firstName;
        private string _lastName;
        private readonly ObservableAsPropertyHelper<string> _fullName;

        public PropertyChangedViewModel()
        {
            this.WhenAnyValue(x => x.FirstName,
                    x => x.LastName,
                    (first, last) => $"{first} {last}")
                .ToProperty(this, nameof(FullName), out _fullName, scheduler: RxApp.MainThreadScheduler);
        }

        public string FirstName
        {
            get => _firstName;
            set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }

        public string LastName
        {
            get => _firstName;
            set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }

        public string FullName => _fullName.Value;
    }
}