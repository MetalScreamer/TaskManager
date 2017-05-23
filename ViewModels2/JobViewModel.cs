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
        private Func<INoteViewModel> noteFactory;
        private Func<ITaskViewModel> taskFactory;
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
            set { SetProperty(ref selectedTask, value, v => selectedTask = v); }
        }

        public INoteViewModel SelectedNote
        {
            get { return selectedNote; }
            set { SetProperty(ref selectedNote, value, v => selectedNote = v); }
        }

        public ObservableCollection<INoteViewModel> Notes { get; } = new ObservableCollection<INoteViewModel>();
        public ObservableCollection<ITaskViewModel> Tasks { get; } = new ObservableCollection<ITaskViewModel>();

        public DelegateCommand AddNote { get; }
        public DelegateCommand RemoveNote { get; }

        public DelegateCommand AddTask { get; }
        public DelegateCommand RemoveTask { get; }

        public JobViewModel(IContentManager contentManager, IJob job, Func<INote, INoteViewModel> existingNoteFactory, Func<INoteViewModel> newNoteFactory, Func<ITask, ITaskViewModel> existingTaskFactory, Func<ITaskViewModel> newTaskFactory)
        {
            this.contentManager = contentManager;
            this.job = job;
            noteFactory = newNoteFactory;
            taskFactory = newTaskFactory; 

            AddNote = new DelegateCommand(_ => DoAddNote());
            RemoveNote = new DelegateCommand(_ => DoRemoveNote(), _ => CanRemoveNote());

            AddTask = new DelegateCommand(_ => DoAddTask());
            RemoveTask = new DelegateCommand(_ => DoRemoveTask(), _ => CanRemoveTask());

            foreach(var note in job.Notes)
            {
                Notes.Add(existingNoteFactory(note));
            }

            foreach(var task in job.Tasks)
            {
                Tasks.Add(existingTaskFactory(task));
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
            var newTask = taskFactory();
            newTask.Name = Tasks.GetUniqueName("Task");
            ExecuteCommand(new UndoCommand(
                () => Tasks.Add(newTask),
                () => Tasks.Remove(newTask)));
        }

        private bool CanRemoveNote()
        {
            throw new NotImplementedException();
        }

        private void DoRemoveNote()
        {
            throw new NotImplementedException();
        }

        private void DoAddNote()
        {
            throw new NotImplementedException();
        }
    }
}
