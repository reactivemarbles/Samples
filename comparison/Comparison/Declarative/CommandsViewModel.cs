using System.Reactive;
using ReactiveUI;

namespace Declarative
{
    public class CommandsViewModel
    {
        public ReactiveCommand<Unit, Unit> MyCommand { get; }
    }
}