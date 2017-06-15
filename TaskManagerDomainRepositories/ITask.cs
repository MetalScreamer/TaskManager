using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DomainRepositories
{
    public interface ITask : IBusinessEntity
    {
        string Name { get; set; }
        string Description { get; set; }
        TaskPriority Priority { get; set; }
        TaskStatus Status { get; set; }
        DateTime DueDate { get; set; }

        IList<INote> Notes { get; }
        IList<ITask> Children { get; }

        ITaskParent Parent { get; }
    }

    public interface ITaskParent
    {
        long ParentId { get; }
        string ParentType { get; }
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
