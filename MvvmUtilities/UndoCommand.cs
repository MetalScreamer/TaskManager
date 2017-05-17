using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.MvvmUtilities
{
    public class UndoCommand
    {
        public Action Do { get; }
        public Action Undo { get; }

        public UndoCommand(Action _do, Action undo)
        {
            Do = _do;
            Undo = undo;
        }
    }
}
