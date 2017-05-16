using System.Collections.Generic;

namespace Jsc.TaskManager.ViewModels
{
    public interface IJob
    {
        string Name { get; set; }
        string Description { get; set; }
        IList<ITask> Tasks { get; }
        IList<INote> Notes { get; }
    }
}