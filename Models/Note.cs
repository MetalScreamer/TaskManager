using System;

namespace Jsc.TaskManager.Models
{
    public interface INote
    {
        DateTime DateTime { get; set; }
        string Text { get; set; }
    }

    internal class Note
    {
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
    }
}