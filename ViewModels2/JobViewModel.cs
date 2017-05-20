using Jsc.MvvmUtilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.ViewModels
{
    public interface IJobViewModel
    {
        string Name { get; set; }
        string Description { get; set; }
        ObservableCollection<ITaskViewModel> Tasks { get; }
        ObservableCollection<INoteViewModel> Notes { get; }
    }

    public class JobViewModel : UndoableViewModel, IJobViewModel
    {
        private string description;
        private string name;

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

        public ObservableCollection<INoteViewModel> Notes { get; } = new ObservableCollection<INoteViewModel>();
        public ObservableCollection<ITaskViewModel> Tasks { get; } = new ObservableCollection<ITaskViewModel>();
    }
}
