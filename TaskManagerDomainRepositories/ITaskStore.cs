using System;

namespace Jsc.TaskManager.DomainRepositories
{
    public interface ITaskStore : IStorageEntity, IParent
    {
        long TaskId { get; }
        string Name { get; set; }
        string Description { get; set; }
        int Priority { get; set; }
        int Status { get; set; }
        DateTime DueDate { get; set; }        
    }
}
