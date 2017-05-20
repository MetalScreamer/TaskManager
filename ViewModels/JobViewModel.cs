using Jsc.TaskManager.Models;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using Jsc.MvvmUtilities;

namespace Jsc.TaskManager.ViewModels
{
    public interface IJobViewModel
    {

    }

    public class JobViewModel : ViewModelBase, IJobViewModel
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
            Name = job.Name;
            Description = job.Description;
            PopulateNotes(job.Notes);
            PopulateTasks(job.Tasks);
        }

        private void PopulateTasks(IEnumerable<ITask> tasks)
        {
            //throw new NotImplementedException();
        }

        private void PopulateNotes(IEnumerable<INote> notes)
        {
            //throw new NotImplementedException();
        }
    }
}