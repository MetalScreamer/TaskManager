using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.Services
{
    public interface ITaskService
    {
        IEnumerable<IJob> GetAllJobs();
        IJob GetJob(long jobId);

        IJob CreateJob();

        void Save(params IJob[] jobs);
        void Remove(params IJob[] jobs);
        void Save(params ITask[] tasks);
        void Remove(params Task[] tasks);
        void Save(params INote[] notes);
        void Remove(params INote[] notes);

        ITask AddTask(IJob job);
        ITask AddTask(ITask task);
        INote AddNote(IJob job);
        INote AddNote(ITask task);
    }
}
