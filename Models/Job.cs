using Jsc.TaskManager.DomainRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Jsc.TaskManager.Models
{
    

    public class Job : IJob
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ITask> Tasks { get; }
        public IList<INote> Notes { get; }

        public Job()
        {
            var tasks = new ObservableCollection<ITask>();
            Tasks = tasks;
            tasks.CollectionChanged += Tasks_CollectionChanged;

            var notes = new ObservableCollection<INote>();
            Notes = notes;
            notes.CollectionChanged += Notes_CollectionChanged;
        }

        private void Notes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void Tasks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        long IBusinessEntity.Id { get; set; }
    }
}
