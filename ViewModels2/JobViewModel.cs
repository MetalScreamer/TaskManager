using Jsc.MvvmUtilities;
using Jsc.TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.ViewModels
{
    public interface IJobViewModel : IHasName
    {
        string Description { get; set; }
        ObservableCollection<ITaskViewModel> Tasks { get; }
        ObservableCollection<INoteViewModel> Notes { get; }
    }

    public class JobViewModel : UndoableViewModel, IJobViewModel
    {
        private string description;
        private string name;
        private IContentManager contentManager;
        private IJob job;
        private Func<IContentManager, INoteViewModel> noteFactory;
        private Func<IContentManager, ITaskViewModel> taskFactory;
        private ITaskViewModel selectedTask;
        private INoteViewModel selectedNote;

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value, v => description = v); }
        }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value, v => name = v); }
        }

        public ITaskViewModel SelectedTask
        {
            get { return selectedTask; }
            set
            {
                SetProperty(ref selectedTask, value, v => selectedTask = v);
                RemoveTask.RaiseCanExecuteChanged();
                RebuildTasksContextMenu();
            }
        }

        public INoteViewModel SelectedNote
        {
            get { return selectedNote; }
            set
            {
                SetProperty(ref selectedNote, value, v => selectedNote = v);
                RemoveNote.RaiseCanExecuteChanged();
                RebuildNotesContextMenu();
            }
        }        

        public ObservableCollection<INoteViewModel> Notes { get; } = new ObservableCollection<INoteViewModel>();
        public ObservableCollection<ITaskViewModel> Tasks { get; } = new ObservableCollection<ITaskViewModel>();

        public DelegateCommand AddNote { get; }
        public DelegateCommand RemoveNote { get; }

        public DelegateCommand AddTask { get; }
        public DelegateCommand RemoveTask { get; }

        public ObservableCollection<MenuItem> NotesContextMenu { get; } = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> TasksContextMenu { get; } = new ObservableCollection<MenuItem>();

        public JobViewModel(
            IContentManager contentManager, 
            IJob job, 
            Func<IContentManager, INote, INoteViewModel> existingNoteFactory, 
            Func<IContentManager, INoteViewModel> newNoteFactory, 
            Func<IContentManager, ITask, ITaskViewModel> existingTaskFactory, 
            Func<IContentManager, ITaskViewModel> newTaskFactory)
        {
            this.contentManager = contentManager;
            this.job = job;
            noteFactory = newNoteFactory;
            taskFactory = newTaskFactory;

            AddNote = new DelegateCommand(_ => DoAddNote());
            RemoveNote = new DelegateCommand(_ => DoRemoveNote(), _ => CanRemoveNote());

            AddTask = new DelegateCommand(_ => DoAddTask());
            RemoveTask = new DelegateCommand(_ => DoRemoveTask(), _ => CanRemoveTask());

            foreach (var note in job.Notes)
            {
                Notes.Add(existingNoteFactory(contentManager, note));
            }

            foreach (var task in job.Tasks)
            {
                Tasks.Add(existingTaskFactory(contentManager, task));
            }
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
                () => Tasks.Add(selectedTask)));
        }

        private void DoAddTask()
        {
            var newTask = taskFactory(contentManager);
            newTask.Name = Tasks.GetUniqueName("Task");
            ExecuteCommand(new UndoCommand(
                () => Tasks.Add(newTask),
                () => Tasks.Remove(newTask)));
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
            newNote.DateTime = DateTime.Now;

            ExecuteCommand(new UndoCommand(
                () => Notes.Add(newNote),
                () => Notes.Remove(newNote)));
        }

        private void RebuildNotesContextMenu()
        {
            NotesContextMenu.Clear();

            NotesContextMenu.Add(new MenuItem() { Text = "Edit Note", Command = new DelegateCommand(_ => EditNote()) });
        }

        private void EditNote()
        {
            contentManager.Load(SelectedNote);
        }

        private void RebuildTasksContextMenu()
        {
            TasksContextMenu.Clear();

            TasksContextMenu.Add(new MenuItem() { Text = "Edit Task", Command = new DelegateCommand(_ => EditTask()) });
        }

        private void EditTask()
        {
            contentManager.Load(SelectedTask); 
        }
    }
}
