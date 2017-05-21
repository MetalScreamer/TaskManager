using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.ViewModels
{
    public interface IContentManager
    {
        void LoadContent(object content);
        void Unload(object content);
    }
}
