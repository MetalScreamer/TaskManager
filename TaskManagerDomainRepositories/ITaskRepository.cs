using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DomainRepositories
{
    public interface ITaskRepository
    {
        ITaskStore Create(IParent parent);
        void Add(params ITaskStore[] items);
        void Remove(params ITaskStore[] items);
        void Update(params ITaskStore[] items);
        IEnumerable<ITaskStore> GetAll();
        ITaskStore GetById(long taskId);

        IEnumerable<ITaskStore> GetTasks(IParent parent);
    }
}
