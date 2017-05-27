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
        ITask Task { get; }

        string Description { get; set; }
        DateTime DueDate { get; set; }
        TaskPriority Priority { get; set; }
        TaskStatus Status { get; set; }

        INoteListViewModel Notes { get; }
        ITaskListViewModel Tasks { get; }
    }

    public class TaskViewModel : UndoableViewModel, ITaskViewModel
    {
        private string name;
        private string description;
        private DateTime dueDate;
        private TaskPriority priority;
        private TaskStatus status;

        public ITask Task { get; }

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

        public INoteListViewModel Notes { get; }
        public ITaskListViewModel Tasks { get; }

        public TaskViewModel(
            ITask task,
            IContentManager contentManager,
            Func<IContentManager, ITaskViewModel, INoteListViewModel> noteListFactory,
            Func<IContentManager, ITaskViewModel, ITaskListViewModel> taskListFactory)
        {
            this.Task = task;

            Tasks = taskListFactory(contentManager, this);
            Notes = noteListFactory(contentManager, this);

            Name = task.Name;
            Description = task.Description;
            DueDate = task.DueDate;
            Priority = task.Priority;
            Status = task.Status;
        }
    }
}
