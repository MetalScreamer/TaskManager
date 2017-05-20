using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jsc.TaskManager.Models
{
    public interface INote
    {
        DateTime DateTime { get; set; }
        string Text { get; set; }
    }

    public class Note : INote
    {
        public long NoteId { get; private set; }

        public DateTime DateTime { get; set; }
        public string Text { get; set; }
    }    
}