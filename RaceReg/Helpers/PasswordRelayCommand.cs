//Code obtained from: https://code.msdn.microsoft.com/windowsdesktop/Get-Password-from-df012a86

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;


namespace RaceReg.Helpers
{
    public class PasswordRelayCommand : ICommand
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {

        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute", "Execute cannot be null.");
            }

            _Execute = execute;
            _CanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            //add { CommandManager.RequerySuggested += value; }
            //remove { CommandManager.RequerySuggested -= value; }
            add { }
            remove { }
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_CanExecute == null)
            {
                return true;
            }

            return _CanExecute(parameter);
        }
    }
}
