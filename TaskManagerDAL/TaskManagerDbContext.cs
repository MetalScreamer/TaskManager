using Jsc.TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Jsc.TaskManager.DAL
{
    public class TaskManagerDbContext : DbContext, ITaskManagerDbContext
    {
        public static void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TaskManagerDbContext>());
        }

        public DbSet<Job> Jobs { get; set; } 
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Note> Notes { get; set; }

        public TaskManagerDbContext() : base()
        {
            
        }

        IEnumerable<IJob> ITaskManagerDbContext.Jobs { get { return Jobs; } }
    }
}
