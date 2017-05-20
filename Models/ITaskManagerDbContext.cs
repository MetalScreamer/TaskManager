using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.Models
{
    public interface ITaskManagerDbContext
    {
        IEnumerable<IJob> Jobs { get; }
    }
}
