using System.Collections.Generic;

namespace Jsc.TaskManager.Models
{
    public class Job
    {
        public long JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Task> Tasks { get; } = new List<Task>();
        public IList<Note> notes { get; } = new List<Note>();
    }
}
