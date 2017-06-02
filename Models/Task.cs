using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.Models
{
    public interface ITask
    {
        long TaskId { get; }

        string Name { get; set; }
        string Description { get; set; }
        TaskPriority Priority { get; set; }
        TaskStatus Status { get; set; }
        DateTime DueDate { get; set; }

        IEnumerable<INote> Notes { get; }
        void AddNote(INote note);
        void RemoveNote(INote note);

        IEnumerable<ITask> Children { get; }
        void AddChild(ITask task);
        void RemoveChild(ITask task);

        IJob ParentJob { get; set; }
        ITask ParentTask { get; set; }
    }

    public class Task : ITask
    {
        public long TaskId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now;

        public virtual List<Note> Notes { get; } = new List<Note>();
        public virtual List<Task> Children { get; } = new List<Task>();

        public long? ParentJobId { get; private set; }
        [ForeignKey("ParentJobId")]
        public virtual Job ParentJob { get; set; }

        public long? ParentTaskId { get; private set; }
        [ForeignKey("ParentTaskId")]
        public virtual Task ParentTask { get; set; }

        IEnumerable<INote> ITask.Notes
        {
            get
            {
                //don't return list and allow client to hack.
                return Notes.OfType<INote>();
            }
        }
        IEnumerable<ITask> ITask.Children
        {
            get
            {
                //don't return list and allow client to hack.
                return Children.OfType<ITask>();
            }
        }

        IJob ITask.ParentJob
        {
            get { return ParentJob; }
            set { ParentJob = (Job)value; }
        }

        ITask ITask.ParentTask
        {
            get { return ParentTask; }
            set { ParentTask = (Task)value; }
        }

        void ITask.AddChild(ITask task)
        {
            task.ParentTask = this;
            Children.Add((Task)task);
        }

        void ITask.RemoveChild(ITask task)
        {
            Children.Remove((Task)task);
        }

        void ITask.AddNote(INote note)
        {
            note.ParentTask = this;
            Notes.Add((Note)note);
        }

        void ITask.RemoveNote(INote note)
        {
            Notes.Remove((Note)note);
        }
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
