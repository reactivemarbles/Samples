using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imperative
{
    public class InteractionsViewModel : ViewModelBase
    {
        public ICommand Command { get; }

        async Task Execute()
        {
            Device.BeginInvokeOnMainThread(async () => { await Task.CompletedTask;  });
        }
    }
}