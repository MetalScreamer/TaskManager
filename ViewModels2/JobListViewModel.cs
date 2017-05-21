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
        ObservableCollection<IJobViewModel> Jobs { get; }
        DelegateCommand AddJob { get; }
        DelegateCommand RemoveJob { get; }
        IJobViewModel SelectedJob { get; set; }
    }

    public class JobListViewModel : ViewModelBase, IJobListViewModel
    {
        private IContentManager contentManager;
        private IJobViewModel selectedJob;
        private bool gridMenuVisible;

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

        public JobListViewModel(ITaskManagerDbContext db, IContentManager contentManager, Func<IContentManager, IJob, IJobViewModel> existingJobFactory, Func<IContentManager, IJobViewModel> newJobFactory)
        {
            this.contentManager = contentManager;

            foreach (var job in db.Jobs)
            {
                Jobs.Add(existingJobFactory(contentManager, job));
            }

            AddJob = new DelegateCommand(_ => DoAddJob(newJobFactory));
            RemoveJob = new DelegateCommand(_ => Jobs.Remove(SelectedJob), _ => SelectedJob != null);
        }

        private void DoAddJob(Func<IContentManager, IJobViewModel> jobFactory)
        {
            var job = jobFactory(contentManager);
            job.Name = Jobs.GetUniqueName("Job");
            Jobs.Add(job);
        }
    }
}
