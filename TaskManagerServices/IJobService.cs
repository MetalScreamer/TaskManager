using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.Services
{
    public interface IJobService
    {
        IEnumerable<IJob> GetAllJobs();
    }
}
