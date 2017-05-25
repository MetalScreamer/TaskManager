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
        ObservableCollection<ITaskViewModel> Tasks { get; }

        INoteViewModel SelectedNote { get; set; }
        DelegateCommand AddNote { get; }
        DelegateCommand RemoveNote { get; }

        ITaskViewModel SelectedTask { get; set; }
        DelegateCommand AddTask { get; }
        DelegateCommand RemoveTask { get; }
    }

    public class TaskViewModel : UndoableViewModel, ITaskViewModel
    {
        private string name;
        private string description;
        private DateTime dueDate;
        private TaskPriority priority;
        private TaskStatus status;

        private ITask task;
        private Func<IContentManager, ITaskViewModel> taskFactory;
        private Func<IContentManager, INoteViewModel> noteFactory;
        private ITaskViewModel selectedTask;
        private INoteViewModel selectedNote;
        private IContentManager contentManager;

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
            set
            {
                SetProperty(ref selectedNote, value, v => selectedNote = v);
                RemoveNote.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<INoteViewModel> Notes { get; } = new ObservableCollection<INoteViewModel>();
        public ObservableCollection<ITaskViewModel> Tasks { get; } = new ObservableCollection<ITaskViewModel>();

        public DelegateCommand AddTask { get; }
        public DelegateCommand RemoveTask { get; }

        public DelegateCommand AddNote { get; }
        public DelegateCommand RemoveNote { get; }
               
        public TaskViewModel(
            ITask task,
            IContentManager contentManager,
            Func<IContentManager, ITask, ITaskViewModel> existingTaskFactory, 
            Func<IContentManager, ITaskViewModel> newTaskFactory, 
            Func<IContentManager, INote, INoteViewModel> existingNoteFactory, 
            Func<IContentManager, INoteViewModel> newNoteFactory)
        {
            this.task = task;
            this.contentManager = contentManager;
            Name = task.Name;
            Description = task.Description;
            DueDate = task.DueDate;
            Priority = task.Priority;
            Status = task.Status;
            taskFactory = newTaskFactory;
            noteFactory = newNoteFactory;

            AddTask = new DelegateCommand(_ => DoAddTask());
            RemoveTask = new DelegateCommand(_ => DoRemoveTask(), _ => CanRemoveTask());

            AddNote = new DelegateCommand(_ => DoAddNote());
            RemoveNote = new DelegateCommand(_ => DoRemoveNote(), _ => CanRemoveNote());

            foreach (var child in task.Children)
            {
                Tasks.Add(existingTaskFactory(contentManager, child));
            }

            foreach (var note in task.Notes)
            {
                Notes.Add(existingNoteFactory(contentManager, note));
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
            var newNote = noteFactory(contentManager);
            ExecuteCommand(new UndoCommand(
                () => Notes.Add(newNote),
                () => Notes.Remove(newNote)));
        }

        private bool CanRemoveTask()
        {
            return SelectedTask != null;
        }

        private void DoRemoveTask()
        {
            var selectedTask = SelectedTask;
            ExecuteCommand(new UndoCommand(
                () => Tasks.Remove(selectedTask),
                () => Tasks.Add(SelectedTask)));
        }

        private void DoAddTask()
        {
            var newTask = taskFactory(contentManager);
            newTask.Name = Tasks.GetUniqueName("Task");
        }
    }
}
