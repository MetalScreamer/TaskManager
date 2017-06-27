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
        private DomainRepository<DbTask> repo = new DomainRepository<DbTask>();

        public ITaskStore Create(IParent parent)
        {
            throw new NotImplementedException();
        }

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
            return repo.GetSingle(t => t.TaskId == taskId);
        }

        public IEnumerable<ITaskStore> GetTasks(IParent parent)
        {
            return repo.GetList(t => t.ParentTypeId == parent.ParentTypeId && t.ParentRecordId == parent.ParentRecordId);
        }
    }
}
