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
        IStorage<IJob>, 
        IStorage<INote>,
        IStorage<ITask>
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

            foreach (var note in job.Notes.ToList())
            {
                Remove(note);
            }

            foreach (var task in job.Tasks.ToList())
            {
                Remove(task);
            }

            if (Jobs.Any(j => job.JobId == j.JobId))
            {
                Jobs.Remove(job);
            }
        }

        public void Save(INote obj)
        {
            var note = (Note)obj;
            if (!Notes.Any(n => n.NoteId == note.NoteId))
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
            var task = (Task)obj;            

            if (!Tasks.Any(t => t.TaskId == task.TaskId))
            {
                Tasks.Add(task);
            }
        }

        public void Remove(ITask obj)
        {
            var task = (Task)obj;

            foreach (var note in task.Notes.ToList())
            {
                Remove(note);
            }

            foreach (var child in task.Children.ToList())
            {
                Remove(child);
            }

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
