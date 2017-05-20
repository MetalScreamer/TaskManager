using Jsc.MvvmUtilities;
using Jsc.TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.ViewModels2
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
        public ObservableCollection<IJobViewModel> Jobs { get; } = new ObservableCollection<IJobViewModel>();
        public DelegateCommand AddJob { get; }
        public DelegateCommand RemoveJob { get; }
        public IJobViewModel SelectedJob { get; set; }

        public JobListViewModel(ITaskManagerDbContext db, Func<IJob, IJobViewModel> existingJobFactory, Func<IJobViewModel> newJobFactory)
        {
            foreach (var job in db.Jobs)
            {
                Jobs.Add(existingJobFactory(job));
            }

            AddJob = new DelegateCommand(_ => Jobs.Add(newJobFactory()));
            RemoveJob = new DelegateCommand(_ => Jobs.Remove(SelectedJob), _ => SelectedJob != null);
        }
    }
}
