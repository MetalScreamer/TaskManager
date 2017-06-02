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
        public virtual List<Task> Tasks { get; } = new List<Task>();
        public virtual List<Note> Notes { get; } = new List<Note>();

        IEnumerable<ITask> IJob.Tasks
        {
            get { return Tasks; }
        }

        IEnumerable<INote> IJob.Notes
        {
            get { return Notes; }
        }

        void IJob.AddTask(ITask task)
        {
            task.ParentJob = this;
            Tasks.Add((Task)task);
        }

        void IJob.RemoveTask(ITask task)
        {
            Tasks.Remove((Task)task);
        }

        void IJob.AddNote(INote note)
        {
            note.ParentJob = this;
            Notes.Add((Note)note);
        }

        public void RemoveNote(INote note)
        {
            Notes.Remove((Note)note);
        }
    }
}
