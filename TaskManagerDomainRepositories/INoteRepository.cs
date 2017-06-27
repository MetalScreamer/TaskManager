using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DomainRepositories
{
    public interface INoteRepository
    {
        INoteStore Create(IParent parent);
        void Add(params INoteStore[] items);
        void Remove(params INoteStore[] items);
        void Update(params INoteStore[] items);
        IEnumerable<INoteStore> GetAll();
        INoteStore GetById(long noteId);

        IEnumerable<INoteStore> GetTasks(IParent parent);
    }
}
