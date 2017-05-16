using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jsc.Wpf
{
    public class CommandWrapper : ICommand
    {
        private Action<object> execute;
        private Predicate<object> canExecute;

        public event EventHandler CanExecuteChanged;

        public CommandWrapper(Jsc.MvvmUtilities.ICommand command)
        {
            this.execute = command.Execute;
            this.canExecute = command.CanExecute;

            command.CanExecuteChanged += Command_CanExecuteChanged;            
        }

        private void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            this.CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
