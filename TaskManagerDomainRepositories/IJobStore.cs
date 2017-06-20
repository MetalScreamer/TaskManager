using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DomainRepositories
{
    public interface IJobStore : IStorageEntity
    {
        long JobId { get; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
