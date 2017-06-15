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
    class DbJob
    {
        [Key]
        public long JobId { get; private set; }

        public string Name { get; set; }
        public string Description { get; set; }       
    }
}
