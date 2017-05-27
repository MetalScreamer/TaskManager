using Jsc.MvvmUtilities;
using Jsc.TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Jsc.TaskManager.DAL
{
    public class TaskManagerDbContext : DbContext, IDataAccess<IJob>
    {
        public static void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TaskManagerDbContext>());
        }

        public void Save(IJob obj)
        {
            var job = (Job)obj;
            if (!Jobs.Contains(job))
            {
                Jobs.Add(job);
            }            
        }

        public void Remove(IJob obj)
        {
            var job = (Job)obj;
            if (Jobs.Contains(job))
            {
                Jobs.Remove(job);
            }
        }

        public void Commit()
        {
            this.SaveChanges();
        }

        public DbSet<Job> Jobs { get; set; } 
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Note> Notes { get; set; }

        public TaskManagerDbContext() : base()
        {
            
        }
    }
}
