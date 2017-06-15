using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DAL
{
    public class Repository : DbContext
    {
        DbSet<DbJob> Jobs { get; set; }
        DbSet<DbTask> Tasks { get; set; }
        DbSet<DbNote> Notes { get; set; }
    }
}
