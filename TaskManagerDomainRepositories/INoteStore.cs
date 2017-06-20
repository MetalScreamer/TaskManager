using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DomainRepositories
{
    public interface INoteStore : IStorageEntity
    {
        long NoteId { get; }
        DateTime DateAdded { get; set; }
        string Text { get; set; }

        string ParentTypeId { get; set; }
        long ParentRecordId { get; set; }
    }
}
