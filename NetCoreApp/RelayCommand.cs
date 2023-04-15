using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;

namespace NetCoreApp
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<object> action;
        private readonly Func<object, bool> canExecute;
        public RelayCommand(Action<object> _action, Func<object, bool> _canExecute)
        {
            action = _action;
            canExecute = _canExecute;
        }
        
        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
