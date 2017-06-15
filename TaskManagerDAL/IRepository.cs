using Jsc.TaskManager.DomainRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DAL
{
    public interface IRepository<T> where T: IBusinessEntity
    {
        T Find(long id);
        
        void Save(T entity);        
        void Remove(T entity);
    }
}
