using Jsc.TaskManager.DomainRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DAL
{
    public interface IRepository<TStorageEntity> 
        where TStorageEntity : IStorageEntity
    {
        TStorageEntity Find(long id);
        IEnumerable<TStorageEntity> Find(Predicate<TStorageEntity> predicate);
        
        void Save(TStorageEntity entity);        
        void Remove(TStorageEntity entity);
    }
}
