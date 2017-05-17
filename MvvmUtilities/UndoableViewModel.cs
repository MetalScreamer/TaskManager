using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.MvvmUtilities
{
    public abstract class UndoableViewModel : ViewModelBase
    {
        private Stack<UndoCommand> UndoStack { get; } = new Stack<UndoCommand>();
        private Stack<UndoCommand> RedoStack { get; } = new Stack<UndoCommand>();

        public DelegateCommand Undo { get; }
        public DelegateCommand Redo { get; }

        public UndoableViewModel()
        {
            Undo = new DelegateCommand(_ => DoUndo(), _ => CanUndo());
            Redo = new DelegateCommand(_ => DoRedo(), _ => CanRedo());
        }

        private bool CanRedo()
        {
            return RedoStack.Count > 0;
        }

        private void DoRedo()
        {
            var redoCommand = RedoStack.Pop();
            redoCommand.Do();
            UndoStack.Push(redoCommand);
            StackStatesChanged();
        }        

        private bool CanUndo()
        {
            return UndoStack.Count > 0;
        }

        private void DoUndo()
        {
            var undoCommand = UndoStack.Pop();
            undoCommand.Undo();
            RedoStack.Push(undoCommand);
            StackStatesChanged();
        }

        protected void ExecuteCommand(UndoCommand cmd)
        {
            RedoStack.Clear();
            cmd.Do();
            UndoStack.Push(cmd);
            StackStatesChanged();
        }

        private void StackStatesChanged()
        {
            Redo.RaiseCanExecuteChanged();
            Undo.RaiseCanExecuteChanged();
        }
    }
}
