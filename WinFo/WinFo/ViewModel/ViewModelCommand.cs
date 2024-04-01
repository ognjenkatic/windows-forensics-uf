using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WinFo.ViewModel
{
    /// <summary>
    /// Command implementation for use when calling methods from the UI
    /// </summary>
    public class ViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = delegate { };

        protected Predicate<object> _canExecute;
        protected Action<object> _commandToExecute;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public ViewModelCommand(Action<object> command)
        {
            _commandToExecute = command;
        }

        public ViewModelCommand(Action<object> commandToExecute, Predicate<object> canExecute)
        {
            _commandToExecute = commandToExecute;
            _canExecute = canExecute;
        }
        
        public bool CanExecute(object parameter)
        {

            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }

            if (_commandToExecute != null)
            {
                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            _commandToExecute?.Invoke(parameter);
        }
    }
}
