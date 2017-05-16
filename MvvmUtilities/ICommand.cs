using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.MvvmUtilities
{
    public interface ICommand
    {
        void Execute(object parameter);
        bool CanExecute(object parameter);

        event EventHandler CanExecuteChanged;
    }
}
