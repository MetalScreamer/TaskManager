using Jsc.TaskManager.DomainRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Jsc.TaskManager.Models
{
    public class Task : ITask
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now;

        public IList<INote> Notes { get; } = new ObservableCollection<INote>();
        public IList<ITask> Children { get; } = new ObservableCollection<ITask>();

        public ITaskParent Parent { get; }

        long IBusinessEntity.Id { get; set; }
    }    

    
}
