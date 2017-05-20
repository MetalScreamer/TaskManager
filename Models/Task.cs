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
        IEnumerable<INote> Notes { get; }
        IEnumerable<ITask> Children { get; }
        TaskPriority Priority { get; set; }
        TaskStatus Status { get; set; }
        DateTime DueDate { get; set; }

        void AddChild(ITask task);
        void RemoveChild(ITask task);
        void AddNote(INote note);
        void RemoveNote(INote note);
    }

    public class Task : ITask
    {
        public long TaskId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Note> Notes { get; } = new List<Note>();
        public List<Task> Children { get; } = new List<Task>();
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }

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

        public void AddChild(ITask task)
        {
            Children.Add((Task)task);
        }

        public void RemoveChild(ITask task)
        {
            Children.Remove((Task)task);
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
