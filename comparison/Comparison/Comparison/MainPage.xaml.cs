using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Comparison.Common;
using ReactiveUI;
using Xamarin.Forms;

namespace Comparison
{
    public partial class MainPage
    {
        private static readonly Dictionary<string, Func<IThreadSafeViewModel>> ViewModelFactory =
            new Dictionary<string, Func<IThreadSafeViewModel>>
            {
                {"Imperative", () => new Imperative.ThreadSafeViewModel()},
                {"Declarative", () => new Declarative.ThreadSafeViewModel()},
            };

        public MainPage()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.TypePicker.SelectedItem)
                .Where(x => x != null)
                .Cast<string>()
                .Select(x => ViewModelFactory[x]())
                .BindTo(this, x => x.ThreadSafeView.ViewModel);

            this.WhenAnyValue(x => x.ThreadSafeView.ViewModel)
                .Where(x => x != null)
                .Subscribe(_ => this.DisplayAlert("Thing", "The View Model Changed", "Ok"));
        }
    }
}
