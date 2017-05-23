using Jsc.MvvmUtilities;
using Jsc.TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Jsc.TaskManager.ViewModels
{
    public interface ITaskViewModel : IHasName
    {
        string Description { get; set; }
        DateTime DueDate { get; set; }
        TaskPriority Priority { get; set; }
        TaskStatus Status { get; set; }

        ObservableCollection<INoteViewModel> Notes { get; }
        ObservableCollection<ITaskViewModel> Children { get; }
    }

    public class TaskViewModel : UndoableViewModel, ITaskViewModel
    {
        private string name;
        private string description;
        private DateTime dueDate;
        private TaskPriority priority;
        private TaskStatus status;

        private ITask task;
        private Func<ITaskViewModel> taskFactory;
        private Func<INoteViewModel> noteFactory;
        private ITaskViewModel selectedTask;
        private INoteViewModel selectedNote;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value, v => name = v); }
        }

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value, v => description = v); }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set { SetProperty(ref dueDate, value, v => dueDate = v); }
        }

        public TaskPriority Priority
        {
            get { return priority; }
            set { SetProperty(ref priority, value, v => priority = v); }
        }

        public TaskStatus Status
        {
            get { return status; }
            set { SetProperty(ref status, value, v => status = v); }
        }

        public ITaskViewModel SelectedTask
        {
            get { return selectedTask; }
            set { SetProperty(ref selectedTask, value, v => selectedTask = v); }
        }

        public INoteViewModel SelectedNote
        {
            get { return selectedNote; }
            set { SetProperty(ref selectedNote, value, v => selectedNote = v); }
        }

        public ObservableCollection<INoteViewModel> Notes { get; } = new ObservableCollection<INoteViewModel>();
        public ObservableCollection<ITaskViewModel> Children { get; } = new ObservableCollection<ITaskViewModel>();

        public DelegateCommand AddChild { get; }
        public DelegateCommand RemoveChild { get; }

        public DelegateCommand AddNote { get; }
        public DelegateCommand RemoveNote { get; }

        public TaskViewModel(ITask task, Func<ITask, ITaskViewModel> existingTaskFactory, Func<ITaskViewModel> newTaskFactory, Func<INote, INoteViewModel> existingNoteFactory, Func<INoteViewModel> newNoteFactory)
        {
            this.task = task;
            Name = task.Name;
            Description = task.Description;
            DueDate = task.DueDate;
            Priority = task.Priority;
            Status = task.Status;
            taskFactory = newTaskFactory;
            noteFactory = newNoteFactory;

            AddChild = new DelegateCommand(_ => DoAddChild());
            RemoveChild = new DelegateCommand(_ => DoRemoveChild(), _ => CanRemoveChild());

            AddNote = new DelegateCommand(_ => DoAddNote());
            RemoveNote = new DelegateCommand(_ => DoRemoveNote(), _ => CanRemoveNote());

            foreach (var child in task.Children)
            {
                Children.Add(existingTaskFactory(child));
            }

            foreach (var note in task.Notes)
            {
                Notes.Add(existingNoteFactory(note));
            }
        }

        private bool CanRemoveNote()
        {
            return SelectedNote != null;
        }

        private void DoRemoveNote()
        {
            var selectedNote = SelectedNote;
            ExecuteCommand(new UndoCommand(
                () => Notes.Remove(selectedNote),
                () => Notes.Add(selectedNote)));
        }

        private void DoAddNote()
        {
            var newNote = noteFactory();
            ExecuteCommand(new UndoCommand(
                () => Notes.Add(newNote),
                () => Notes.Remove(newNote)));
        }

        private bool CanRemoveChild()
        {
            return SelectedTask != null;
        }

        private void DoRemoveChild()
        {
            var selectedTask = SelectedTask;
            ExecuteCommand(new UndoCommand(
                () => Children.Remove(selectedTask),
                () => Children.Add(SelectedTask)));
        }

        private void DoAddChild()
        {
            var newTask = taskFactory();
            newTask.Name = Children.GetUniqueName("Task");
        }
    }
}
