using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DAL
{
    [Table("Tasks")]
    class DbTask
    {
        [Key]
        public long TaskId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public DateTime DueDate { get; set; }

        public Guid ParentTypeId { get; set; }
        public long ParentRecordId { get; set; }
    }
}
