using System.Collections.Generic;

namespace Jsc.TaskManager.DomainRepositories
{
    public interface IJobRepository 
    {
        IJobStore Create();
        void Add(params IJobStore[] items);
        void Remove(params IJobStore[] items);
        void Update(params IJobStore[] items);
        IEnumerable<IJobStore> GetAll();
        IJobStore GetById(long jobId);
    }
}
