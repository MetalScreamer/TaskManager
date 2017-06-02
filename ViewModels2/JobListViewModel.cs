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
    public interface IJobListViewModel
    {
        IEnumerable<IJobViewModel> Jobs { get; }
        ICommand AddJob { get; }
        ICommand RemoveJob { get; }
        IJobViewModel SelectedJob { get; set; }
        ICommand EditJob { get; }
    }

    public class JobListViewModel : ViewModelBase, IJobListViewModel
    {
        //private IContentManager contentManager;
        private IJobViewModel selectedJob;
        private bool gridMenuVisible = true;
        private IContentManager contentManager;
        
        public ObservableCollection<IJobViewModel> Jobs { get; } = new ObservableCollection<IJobViewModel>();
        public ObservableCollection<MenuItem> JobListMenu { get; } = new ObservableCollection<MenuItem>();

        public DelegateCommand AddJob { get; }
        public DelegateCommand RemoveJob { get; }
        public IJobViewModel SelectedJob
        {
            get { return selectedJob; }
            set
            {
                SetProperty(ref selectedJob, value);
                RemoveJob.RaiseCanExecuteChanged();
                gridMenuVisible = SelectedJob != null;
            }
        }

        IEnumerable<IJobViewModel> IJobListViewModel.Jobs
        {
            get { return Jobs; }
        }

        ICommand IJobListViewModel.AddJob
        {
            get { return AddJob; }
        }

        ICommand IJobListViewModel.RemoveJob
        {
            get { return RemoveJob; }
        }

        public DelegateCommand EditJob { get; }

        ICommand IJobListViewModel.EditJob
        {
            get { return EditJob; }
        }

        public JobListViewModel(
            IContentManager contentManager,
            IEnumerable<IJobViewModel> jobs, 
            Func<IContentManager, IJobViewModel> newJobFactory)
        {
            this.contentManager = contentManager;

            foreach (var job in jobs)
            {
                Jobs.Add(job);
            }

            AddJob = new DelegateCommand(_ => DoAddJob(() => newJobFactory(contentManager)));
            RemoveJob = new DelegateCommand(_ => DoRemoveJob(), _ => CanRemoveJob());
            EditJob = new DelegateCommand(_ => DoEditJob(contentManager));

            JobListMenu.Add(new MenuItem() { Text = "Edit Job", Command = EditJob });
        }

        private void DoEditJob(IContentManager contentManager)
        {
            contentManager.Load(SelectedJob);
        }

        private bool CanRemoveJob()
        {
            return SelectedJob != null;
        }

        private void DoRemoveJob()
        {
            var selectedJob = SelectedJob;

            Jobs.Remove(selectedJob);
            selectedJob.Remove();
        }

        private void DoAddJob(Func<IJobViewModel> jobFactory)
        {
            var job = jobFactory();
            job.Name = Jobs.GetUniqueName("Job");
            Jobs.Add(job);
            SelectedJob = job;
            job.Save();
            contentManager.Load(job);               
        }
        
    }
}
