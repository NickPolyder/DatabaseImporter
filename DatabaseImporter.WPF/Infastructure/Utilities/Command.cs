using System;
using System.Windows.Input;

namespace DatabaseImporter.WPF.Infastructure.Utilities
{
    public class Command : ICommand
    {
        private readonly Action<object> _executeAction;
        private readonly Func<object, bool> _canExecuteFunc;

        public Command(Action<object> executeAction) : this(executeAction, null)
        { }

        public Command(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            _canExecuteFunc = canExecuteFunc ?? ((__) => true);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc?.Invoke(parameter) ?? false; 
        }

        public void Execute(object parameter)
        {
            _executeAction?.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}