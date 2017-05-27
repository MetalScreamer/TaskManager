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
    }

    public class JobListViewModel : ViewModelBase, IJobListViewModel
    {
        private IContentManager contentManager;
        private IJobViewModel selectedJob;
        private bool gridMenuVisible = true;

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

        public bool GridMenuVisible
        {
            get { return gridMenuVisible; }
            set
            {
                if (value && SelectedJob == null) value = false;
                SetProperty(ref gridMenuVisible, value);
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

        public JobListViewModel(
            IContentManager contentManager,
            IEnumerable<IJobViewModel> jobs, 
            Func<IContentManager, IJobViewModel> newJobFactory,
            IDataAccess<IJob> dataAccess)
        {
            this.contentManager = contentManager;

            foreach (var job in jobs)
            {
                Jobs.Add(job);
            }

            JobListMenu.Add(new MenuItem() { Text = "Edit Job", Command = new DelegateCommand(_ => EditJob()) });

            AddJob = new DelegateCommand(_ => DoAddJob(() => newJobFactory(contentManager)));
            RemoveJob = new DelegateCommand(_ => DoRemoveJob(), _ => CanRemoveJob());
        }

        private bool CanRemoveJob()
        {
            return SelectedJob != null;
        }

        private void DoRemoveJob()
        {
            Jobs.Remove(SelectedJob); ;
        }

        private void EditJob()
        {
            contentManager.Load(SelectedJob);
        }

        private void DoAddJob(Func<IJobViewModel> jobFactory)
        {
            var job = jobFactory();
            job.Name = Jobs.GetUniqueName("Job");
            Jobs.Add(job);
        }
        
    }
}
