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
    public class DbTask : ITaskStore
    {
        public const string TYPE_ID = "task";

        [Key]
        public long TaskId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public DateTime DueDate { get; set; }

        public string ParentTypeId { get; set; }
        public long ParentRecordId { get; set; }
    }
}
