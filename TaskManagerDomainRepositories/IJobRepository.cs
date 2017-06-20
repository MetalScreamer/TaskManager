using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DomainRepositories
{
    public interface IJobRepository 
    {
        void Add(params IJobStore[] items);
        void Remove(params IJobStore[] items);
        void Update(params IJobStore[] items);
        IEnumerable<IJobStore> GetAll();
        IJobStore GetById(long jobId);
    }
}
