using Jsc.TaskManager.DomainRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DAL
{
    public class NoteRepository : INoteRepository
    {
        private DomainRepository<DbNote> repo = new DomainRepository<DbNote>();

        public INoteStore Create(IParent parent)
        {
            return new DbNote() { ParentTypeId = parent.ParentTypeId, ParentRecordId = parent.ParentRecordId };
        }

        public void Add(params INoteStore[] items)
        {
            repo.Add(items.Cast<DbNote>().ToArray());
        }

        public void Remove(params INoteStore[] items)
        {
            repo.Remove(items.Cast<DbNote>().ToArray());
        }

        public void Update(params INoteStore[] items)
        {
            repo.Update(items.Cast<DbNote>().ToArray());
        }

        public IEnumerable<INoteStore> GetAll()
        {
            return repo.GetAll();
        }

        public INoteStore GetById(long noteId)
        {
            return repo.GetSingle(n => n.NoteId == noteId);
        }

        public IEnumerable<INoteStore> GetTasks(IParent parent)
        {
            return repo.GetList(n => n.ParentTypeId == parent.ParentTypeId && n.ParentRecordId == parent.ParentRecordId);
        }
    }
}
