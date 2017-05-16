using Jsc.MvvmUtilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.ViewModels
{
    class JobListViewModel
    {
        public ObservableCollection<JobViewModel> Jobs { get; } = new ObservableCollection<JobViewModel>();
        public DelegateCommand AddJob { get; }
        public DelegateCommand RemoveJob { get; }
        public JobViewModel SelectedJob { get; set; }

        public JobListViewModel(IEnumerable<IJob> jobs)
        {

        }
    }
}
