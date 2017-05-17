using System.Collections.Generic;

namespace Jsc.TaskManager.Models
{
    public interface IJob
    {
        long JobId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IList<ITask> Tasks { get; }
        IList<INote> notes { get; }
    }

    internal class Job : IJob
    {
        public long JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ITask> Tasks { get; } = new List<ITask>();
        public IList<INote> notes { get; } = new List<INote>();
    }
}
