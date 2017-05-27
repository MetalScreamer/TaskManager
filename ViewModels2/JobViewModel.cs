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
        IJob Job { get; }
        string Description { get; set; }
        INoteListViewModel Notes { get; }
        ITaskListViewModel Tasks { get; }
    }

    public class JobViewModel : UndoableViewModel, IJobViewModel
    {
        private string description;
        private string name;
        private IContentManager contentManager;
        
        public IJob Job { get; }

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

        public INoteListViewModel Notes { get; }
        public ITaskListViewModel Tasks { get; }

        public JobViewModel(
            IContentManager contentManager, 
            IJob job,
            Func<IContentManager, IJobViewModel, INoteListViewModel> noteListFactory,
            Func<IContentManager, IJobViewModel, ITaskListViewModel> taskListFactory)
        {
            this.contentManager = contentManager;

            Job = job;
            Name = job.Name;
            Description = job.Description;
            Notes = noteListFactory(contentManager, this);
            Tasks = taskListFactory(contentManager, this);
        }
    }
}
