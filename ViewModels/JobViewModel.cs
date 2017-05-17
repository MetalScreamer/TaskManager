using Jsc.MvvmUtilities;
using Jsc.TaskManager.Models;
using System.Collections.ObjectModel;

namespace Jsc.TaskManager.ViewModels
{
    internal class JobViewModel : ViewModelBase
    {
        private string name;
        private string description;
        private IJob job;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public ObservableCollection<TaskViewModel> Tasks { get; } = new ObservableCollection<TaskViewModel>();
        public ObservableCollection<NoteViewModel> Notes { get; } = new ObservableCollection<NoteViewModel>();

        public JobViewModel(IJob job)
        {
            this.job = job;
        }
    }
}