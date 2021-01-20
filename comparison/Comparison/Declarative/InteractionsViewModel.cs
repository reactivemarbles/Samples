using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

namespace Declarative
{
    public class InteractionsViewModel : ReactiveObject
    {
        public InteractionsViewModel()
        {
            Command = ReactiveCommand.CreateFromObservable(Execute);
        }

        public ReactiveCommand<Unit, Unit> Command { get; }

        private IObservable<Unit> Execute() =>
            Observable.Create<Unit>(obsever => Interactions.Alert.Handle(Unit.Default).Subscribe(obsever));
    }

    public static class Interactions
    {
        public static readonly Interaction<Unit, Unit> Alert = new Interaction<Unit, Unit>(RxApp.MainThreadScheduler);
    }
}