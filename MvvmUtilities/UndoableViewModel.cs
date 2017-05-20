using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

        protected void ExecuteCommand(UndoCommand cmd)
        {
            cmd.Do();
            AddCommand(cmd);
        }

        protected void AddCommand(UndoCommand cmd)
        {
            RedoStack.Clear();
            UndoStack.Push(cmd);
            StackStatesChanged();
        }

        protected bool SetProperty<T>(ref T storage, T value, Action<T> setter, [CallerMemberName] string propertyName = null)
        {
            var oldValue = storage;
            if (base.SetProperty(ref storage, value, propertyName))
            {
                AddCommand(new UndoCommand(
                    () =>
                    {
                        setter(value);
                        RaisePropertyChanged(propertyName);
                    },
                    () =>
                    {
                        setter(oldValue);
                        RaisePropertyChanged(propertyName);
                    }));
                return true;
            }
            return false;
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

        private void StackStatesChanged()
        {
            Redo.RaiseCanExecuteChanged();
            Undo.RaiseCanExecuteChanged();
        }
    }
}
