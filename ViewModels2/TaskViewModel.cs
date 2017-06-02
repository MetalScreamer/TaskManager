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

        ICommand OkCommand { get; }
        ICommand CanceCommand { get; }

        void Save();
        void Remove();
    }

    public class TaskViewModel : UndoableViewModel, ITaskViewModel
    {
        private string name;
        private string description;
        private DateTime dueDate;
        private TaskPriority priority;
        private TaskStatus status;
        private IDataAccess<ITask> dal;
        private IContentManager contentManager;      

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

        public DelegateCommand OkCommad { get; }
        public DelegateCommand CancelCommand { get; }

        public ICommand OkCommand
        {
            get { return OkCommad; }
        }

        public ICommand CanceCommand
        {
            get { return CancelCommand; }
        }

        public TaskViewModel(
            ITask task,
            IContentManager contentManager,
            Func<IContentManager, ITaskViewModel, INoteListViewModel> noteListFactory,
            Func<IContentManager, ITaskViewModel, ITaskListViewModel> taskListFactory,
            IDataAccess<ITask> dal)
        {
            this.Task = task;
            this.dal = dal;
            this.contentManager = contentManager;

            Tasks = taskListFactory(contentManager, this);
            Notes = noteListFactory(contentManager, this);
            LoadFromTask(task);

            Tasks.TaskAddedCallback = TaskAdded;

            OkCommad = new DelegateCommand(_ => DoOk());
            CancelCommand = new DelegateCommand(_ => DoCancel());
        }

        private void TaskAdded(ITaskViewModel taskVm)
        {
            Task.AddChild(taskVm.Task);
        }

        private void DoCancel()
        {
            LoadFromTask(Task);
            contentManager.Unload(this);
        }

        private void DoOk()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            WriteToModel(Task);
            dal.Save(Task);
            dal.Commit();
        }

        public void Remove()
        {
            dal.Remove(Task);
            dal.Commit();
        }

        private void LoadFromTask(ITask task)
        {
            Name = task.Name;
            Description = task.Description;
            DueDate = task.DueDate;
            Priority = task.Priority;
            Status = task.Status;
        }

        private void WriteToModel(ITask task)
        {
            task.Name = Name;
            task.Description = Description;
            task.DueDate = DueDate;
            task.Priority = Priority;
            task.Status = Status;
        }
    }
}
