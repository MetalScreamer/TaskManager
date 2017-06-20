using Jsc.TaskManager.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jsc.TaskManager.DomainRepositories;

namespace Jsc.TaskManager.DAL
{
    public class StorageConfiguration : IStorageConfiguration
    {
        public void Initialize()
        {
            TaskMgrDbContext.Initialize();

            //IDomainRepository<IJobStore> n = new DomainRepository<DbJob>();
        }
    }
}
