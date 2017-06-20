using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.Models
{
    public interface IJob : IBusinessEntity
    {
        string Name { get; set; }
        string Description { get; set; }
        IList<ITask> Tasks { get; }
        IList<INote> Notes { get; }
    }
}
