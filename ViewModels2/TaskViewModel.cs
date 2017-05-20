using Jsc.MvvmUtilities;
using Jsc.TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Jsc.TaskManager.ViewModels
{
    public interface ITaskViewModel { }

    public class TaskViewModel : UndoableViewModel, ITaskViewModel
    {
        private string name;
        private string description;
        private DateTime dueDate;
        private TaskPriority priority;
        private TaskStatus status;

        private ITask task;

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

        public ObservableCollection<NoteViewModel> Notes { get; } = new ObservableCollection<NoteViewModel>();
        public ObservableCollection<TaskViewModel> Children { get; } = new ObservableCollection<TaskViewModel>();

        public TaskViewModel(ITask task)
        {
            this.task = task;
            Name = task.Name;
            Description = task.Description;
            DueDate = task.DueDate;
            Priority = task.Priority;
            Status = task.Status;

            PopulateNotes(task.Notes);
            PopulateChildren(task.Children);
        }

        private void PopulateChildren(IEnumerable<ITask> children)
        {
            Children.Clear();
            foreach (var child in children)
            {
                Children.Add(new TaskViewModel(child));
            }
        }

        private void PopulateNotes(IEnumerable<INote> notes)
        {
            Notes.Clear();
            foreach (var note in notes)
            {
                Notes.Add(new NoteViewModel(note));
            }
        }
    }
}
