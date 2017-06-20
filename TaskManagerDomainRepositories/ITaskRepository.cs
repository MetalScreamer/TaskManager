using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DomainRepositories
{
    public interface ITaskRepository
    {
        void Add(params ITaskStore[] items);
        void Remove(params ITaskStore[] items);
        void Update(params ITaskStore[] items);
        IEnumerable<ITaskStore> GetAll();
        ITaskStore GetById(long taskId);

        IEnumerable<ITaskStore> GetTasks(IJobStore job);
        IEnumerable<ITaskStore> GetTasks(ITaskStore task);
    }
}
