using Jsc.TaskManager.DomainRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Jsc.TaskManager.DAL
{
    [Table("Tasks")]
    class DbTask
    {
        [Key]
        public long TaskId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }

        public string ParentTypeId { get; set; }
        public long ParentRecordId { get; set; }
    }
}
