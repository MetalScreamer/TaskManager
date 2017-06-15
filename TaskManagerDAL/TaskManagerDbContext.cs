//using Jsc.MvvmUtilities;
//using Jsc.TaskManager.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;

//namespace Jsc.TaskManager.DAL
//{
//    public class TaskManagerDbContext : 
//        DbContext, 
//        IRepository<IJob>, 
//        IRepository<INote>,
//        IRepository<ITask>
//    {
//        private Func<IJob> jobFactory;
//        private Func<ITask> taskFactory;
//        private Func<INote> noteFactory;

//        DbSet<DbJob> Jobs { get; set; }
//        DbSet<DbTask> Tasks { get; set; }
//        DbSet<DbNote> Notes { get; set; }

//        public TaskManagerDbContext(
//            Func<IJob> jobFactory,
//            Func<ITask> taskFactory,
//            Func<INote> noteFactory) : base("TaskManager")
//        {
//            this.jobFactory = jobFactory;
//            this.taskFactory = taskFactory;
//            this.noteFactory = noteFactory;
//        }

//        public static void Initialize()
//        {
//            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TaskManagerDbContext>());
//        }

//        IJob IRepository<IJob>.Find(long id)
//        {
//            return MakeJob(Jobs.FirstOrDefault(j => j.JobId == id));
//        }

//        private IJob MakeJob(DbJob dbJob)
//        {
//            var result = jobFactory();

//            result.Id = dbJob.JobId;
//            result.Name = dbJob.Name;
//            result.Description = dbJob.Description;

//            return result;
//        }

//        public IEnumerable<IJob> Find(Predicate<DbJob> predicate)
//        {
//            return Jobs.Where(j => predicate(j)).Select(j => MakeJob(j));
//        }

//        public void Save(IJob entity)
//        {
//            throw new NotImplementedException();
//        }

//        public void Remove(IJob entity)
//        {
//            throw new NotImplementedException();
//        }

//        INote IRepository<INote>.Find(long id)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<INote> Find(Predicate<INote> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public void Save(INote entity)
//        {
//            throw new NotImplementedException();
//        }

//        public void Remove(INote entity)
//        {
//            throw new NotImplementedException();
//        }

//        ITask IRepository<ITask>.Find(long id)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<ITask> Find(Predicate<ITask> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public void Save(ITask entity)
//        {
//            throw new NotImplementedException();
//        }

//        public void Remove(ITask entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
