using Jsc.MvvmUtilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.ViewModels
{
    public interface ITaskListViewModel : IHasName
    {
        IEnumerable<ITaskViewModel> Tasks { get; }
        ITaskViewModel Selected { get; set; }
        ICommand Add { get; }
        ICommand Remove { get; }
        ICommand EditTask { get; }
        IEnumerable<MenuItem> ContextMenu { get; }
        Action<ITaskViewModel> TaskAddedCallback { get; set; }
    }

    public class TaskListViewModel : ViewModelBase, ITaskListViewModel
    {
        private string name;
        private ITaskViewModel selectedTask;
        private IContentManager contentManager;
        private Action<ITaskViewModel> taskAddedCallback;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public ObservableCollection<ITaskViewModel> Tasks { get; } = new ObservableCollection<ITaskViewModel>();

        public ITaskViewModel Selected
        {
            get { return selectedTask; }
            set
            {
                SetProperty(ref selectedTask, value);
                Remove.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand Add { get; }
        public DelegateCommand Remove { get; }
        public DelegateCommand EditTask { get; }

        public ObservableCollection<MenuItem> ContextMenu { get; } = new ObservableCollection<MenuItem>();

        IEnumerable<ITaskViewModel> ITaskListViewModel.Tasks
        {
            get { return Tasks; }
        }

        IEnumerable<MenuItem> ITaskListViewModel.ContextMenu
        {
            get { return ContextMenu; }
        }

        ICommand ITaskListViewModel.Add
        {
            get { return Add; }
        }

        ICommand ITaskListViewModel.Remove
        {
            get { return Remove; }
        }

        ICommand ITaskListViewModel.EditTask
        {
            get { return EditTask; }
        }

        public Action<ITaskViewModel> TaskAddedCallback
        {
            get { return taskAddedCallback; }
            set { taskAddedCallback = value; }
        }

        public TaskListViewModel(
            IContentManager contentManager,
            IEnumerable<ITaskViewModel> initialTasks,
            Func<IContentManager, ITaskViewModel> taskFactory)
        {
            this.contentManager = contentManager;

            foreach (var task in initialTasks)
            {
                Tasks.Add(task);
            }

            Add = new DelegateCommand(_ => DoAddTask(() => taskFactory(contentManager)));
            Remove= new DelegateCommand(_ => DoRemoveTask(), _ => CanRemoveTask());
            EditTask = new DelegateCommand(_ => DoEditTask());

            ContextMenu.Add(
                new MenuItem()
                {
                    Text = "Edit Task",
                    Command = EditTask
                });
        }

        private void DoEditTask()
        {
            contentManager.Load(Selected);
        }

        private bool CanRemoveTask()
        {
            return Selected != null;
        }

        private void DoRemoveTask()
        {
            Tasks.Remove(Selected);
        }

        private void DoAddTask(Func<ITaskViewModel> taskFactory)
        {
            var newTask = taskFactory();
            newTask.Name = Tasks.GetUniqueName("Task");
            Tasks.Add(newTask);
            TaskAddedCallback?.Invoke(newTask);
            //newTask.Save();            
            //contentManager.Load(newTask);
        }
    }
}
