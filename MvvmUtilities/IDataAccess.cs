using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.MvvmUtilities
{
    public interface IStorage<T>
    {
        void Save(T obj);
        void Remove(T obj);
        void Commit();
    }
}
