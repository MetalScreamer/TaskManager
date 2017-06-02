using Jsc.MvvmUtilities;
using Jsc.TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Jsc.TaskManager.DAL
{
    public class TaskManagerDbContext : 
        DbContext, 
        IDataAccess<IJob>, 
        IDataAccess<INote>,
        IDataAccess<ITask>
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Note> Notes { get; set; }

        public TaskManagerDbContext() : base("TaskManager")
        {

        }

        public static void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TaskManagerDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Task>()
                .HasOptional(t => t.ParentJob)
                .WithMany(j => j.Tasks);
            modelBuilder.Entity<Task>()
                .HasOptional(c => c.ParentTask)
                .WithMany(p => p.Children);
                
        }

        public void Save(IJob obj)
        {
            var job = (Job)obj;
            if (!Jobs.Any(j => job.JobId == j.JobId))
            {
                Jobs.Add(job);
            }
        }

        public void Remove(IJob obj)
        {
            var job = (Job)obj;
            if (Jobs.Any(j => job.JobId == j.JobId))
            {
                Jobs.Remove(job);
            }
        }

        public void Save(INote obj)
        {
            var note = (Note)obj;
            if (Notes.Any(n => n.NoteId == note.NoteId))
            {
                Notes.Add(note);
            }
        }

        public void Remove(INote obj)
        {
            var note = (Note)obj;
            if (Notes.Any(n => n.NoteId == note.NoteId))
            {
                Notes.Remove(note);
            }
        }

        public void Save(ITask obj)
        {
            ////var task = (Task)obj;
            ////if (Tasks.Any(t => t.TaskId == task.TaskId))
            ////{
            ////    Tasks.Add(task);
            ////}
        }

        public void Remove(ITask obj)
        {
            var task = (Task)obj;
            if (Tasks.Any(t => t.TaskId == task.TaskId))
            {
                Tasks.Remove(task);
            }
        }

        public void Commit()
        {
            this.SaveChanges();
        }
    }
}
