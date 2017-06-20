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
    public class DbJob : IJobStore
    {
        public const string TYPE_ID = "job";

        [Key]
        public long JobId { get; private set; }

        public string Name { get; set; }
        public string Description { get; set; }       
    }
}
