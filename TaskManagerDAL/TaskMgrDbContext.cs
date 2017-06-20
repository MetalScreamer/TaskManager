using Jsc.TaskManager.DomainRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DAL
{


    class TaskMgrDbContext : DbContext
    {
        DbSet<DbJob> Jobs { get; set; }
        DbSet<DbTask> Tasks { get; set; }
        DbSet<DbNote> Notes { get; set; }

        public static void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TaskMgrDbContext>());
        }
    }
}
