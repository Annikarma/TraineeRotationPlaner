using System.Windows.Input;

namespace TraineeRotationPlaner.Core
{
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;  // private Action _execute;
        private Func<object, bool> _canExecute; //private Action<bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
       
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }  // eigenes Video dazu
            remove { CommandManager.RequerySuggested -= value; }
        }

        // kommt von der implementierten Schnittstelle ICommand
        public bool CanExecute(object? parameter)
        {
            
           // return _canExecute != null && _canExecute(parameter);
            return _canExecute == null || _canExecute(parameter);
            /*
             _canExecute nicht gesetzt ist, sollte der CanExecute-Handler standardmäßig true zurückgeben. 
            Dadurch können Befehle auch ohne spezifische Einschränkungen ausgeführt werden.
            */
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

    }
    
}
