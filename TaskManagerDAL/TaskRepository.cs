using Jsc.TaskManager.DomainRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DAL
{
    public class TaskRepository : ITaskRepository
    {
        private DomainRepository<DbTask> repo;

        public void Add(params ITaskStore[] items)
        {
            repo.Add(items.Cast<DbTask>().ToArray());
        }

        public void Remove(params ITaskStore[] items)
        {
            repo.Remove(items.Cast<DbTask>().ToArray());
        }

        public void Update(params ITaskStore[] items)
        {
            repo.Update(items.Cast<DbTask>().ToArray());
        }

        public IEnumerable<ITaskStore> GetAll()
        {
            return repo.GetAll();
        }

        public ITaskStore GetById(long taskId)
        {
            return repo.GetSingle(dbTask => dbTask.TaskId == taskId);
        }

        public IEnumerable<ITaskStore> GetTasks(IJobStore job)
        {
            return repo.GetList(dbTask => dbTask.ParentTypeId == DbJob.TYPE_ID && dbTask.ParentRecordId == job.JobId);
        }

        public IEnumerable<ITaskStore> GetTasks(ITaskStore task)
        {
            return repo.GetList(dbTask => dbTask.ParentTypeId == DbTask.TYPE_ID && dbTask.ParentRecordId == task.TaskId);
        }
    }
}
