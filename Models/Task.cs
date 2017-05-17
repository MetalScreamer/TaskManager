using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.Models
{
    public interface ITask
    {
        long TaskId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IList<INote> Notes { get; }
        TaskPriority Priority { get; set; }
        DateTime DueDate { get; set; }
    }

    internal class Task : ITask
    {
        public long TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<INote> Notes { get; } = new List<INote>();
        public TaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }

    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        OnHold,
        Complete
    }
}
