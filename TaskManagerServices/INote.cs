using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.Services
{
    public interface INote : IBusinessEntity
    {
        DateTime DateTime { get; set; }
        string Text { get; set; }
        INoteParent Parent { get; }
    }

    public interface INoteParent
    {
        string parentTypeId { get; }
        long parentId { get; }
    }
}
