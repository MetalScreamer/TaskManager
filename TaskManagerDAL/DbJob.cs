using Jsc.TaskManager.DomainRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DAL
{
    [Table("Jobs")]
    class DbJob : IJobStore
    {
        private const string TYPE_ID = "job";

        [Key]
        public long JobId { get; private set; }

        public string Name { get; set; }
        public string Description { get; set; }

        string IParent.ParentTypeId
        {
            get { return TYPE_ID; }
        }

        long IParent.ParentRecordId
        {
            get { return JobId; }
        }
    }
}
