using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.Models
{
    public class Task
    {
        public long TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Note> Notes { get; } = new List<Note>();
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
