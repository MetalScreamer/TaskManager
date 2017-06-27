using Jsc.TaskManager.DomainRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Jsc.TaskManager.DAL
{
    public class JobRepository : IJobRepository
    {
        private DomainRepository<DbJob> repo = new DomainRepository<DbJob>();

        public IJobStore Create()
        {
            return new DbJob();
        }

        public void Add(params IJobStore[] items)
        {
            repo.Add(items.Cast<DbJob>().ToArray());
        }

        public void Remove(params IJobStore[] items)
        {
            repo.Remove(items.Cast<DbJob>().ToArray());
        }

        public void Update(params IJobStore[] items)
        {
            repo.Update(items.Cast<DbJob>().ToArray());
        }

        public IEnumerable<IJobStore> GetAll()
        {
            return repo.GetAll();
        }        

        public IJobStore GetById(long jobId)
        {
            return repo.GetSingle(dbJob => dbJob.JobId == jobId);
        }
    }
}
