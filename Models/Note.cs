﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jsc.TaskManager.Models
{
    public interface INote
    {
        DateTime DateTime { get; set; }
        string Text { get; set; }

        IJob ParentJob { get; set; }
        ITask ParentTask { get; set; }
    }

    public class Note : INote
    {
        public long NoteId { get; private set; }

        public DateTime DateTime { get; set; }
        public string Text { get; set; }

        public long? ParentJobId { get; private set; } 
        [ForeignKey("ParentJobId")]
        public virtual Job ParentJob { get; set; }

        public long? ParentTaskId { get; private set; }
        [ForeignKey("ParentTaskId")]
        public virtual Task ParentTask { get; set; }

        IJob INote.ParentJob
        {
            get { return ParentJob; }
            set { ParentJob = (Job)value; }
        }

        ITask INote.ParentTask
        {
            get { return ParentTask; }
            set { ParentTask = (Task)value; }
        }
    }    
}