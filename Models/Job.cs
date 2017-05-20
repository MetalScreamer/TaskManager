using System;
using System.Collections.Generic;
using System.Linq;

namespace Jsc.TaskManager.Models
{
    public interface IJob
    {
        long JobId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IEnumerable<ITask> Tasks { get; }
        IEnumerable<INote> Notes { get; }

        void AddTask(ITask task);
        void RemoveTask(ITask task);

        void AddNote(INote note);
        void RemoveNote(INote note);
    }

    public class Job : IJob
    {
        public long JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Task> Tasks { get; } = new List<Task>();
        public List<Note> Notes { get; } = new List<Note>();

        IEnumerable<ITask> IJob.Tasks
        {
            get
            {
                //dont return list and give them a hack
                return Tasks.OfType<ITask>();
            }
        }

        IEnumerable<INote> IJob.Notes
        {
            get
            {
                //dont return list and give them a hack
                return Notes.OfType<INote>();
            }
        }

        public void AddTask(ITask task)
        {
            Tasks.Add((Task)task);
        }

        public void RemoveTask(ITask task)
        {
            Tasks.Remove((Task)task);
        }

        public void AddNote(INote note)
        {
            Notes.Add((Note)note);
        }

        public void RemoveNote(INote note)
        {
            Notes.Remove((Note)note);
        }
    }
}
