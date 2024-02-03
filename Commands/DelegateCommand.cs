using System.Windows.Input;

namespace ClientTestSignalR_2.Commands
{
    /// <summary>
    /// для формирования команд в рамках MVVM для связывания с инструментами интерфейса (кнопками, чекбоксами и т.п.)
    /// </summary>
    public class DelegateCommand : ICommand
    {
        public DelegateCommand(DelegateFunction function)
        {
            _function = function;
        }
        public delegate void DelegateFunction(object? sender);
        DelegateFunction _function;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _function?.Invoke(parameter);
        }
    }
}