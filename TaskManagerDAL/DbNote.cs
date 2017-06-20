using Jsc.TaskManager.DomainRepositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jsc.TaskManager.DAL
{
    [Table("Notes")]
    public class DbNote : INoteStore
    {
        [Key]
        public long NoteId { get; private set; }

        public DateTime DateAdded { get; set; }
        public string Text { get; set; }

        public string ParentTypeId { get; set; }
        public long ParentRecordId { get; set; }
    }
}