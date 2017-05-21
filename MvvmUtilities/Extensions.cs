using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.MvvmUtilities
{
    public static class Extensions
    {
        public static string GetUniqueName(this IEnumerable<IHasName> me, string baseName)
        {
            //initialize to one because the Any method will immediately return true without evaluating the pre-increment when the list is empty;
            var counter = 1;
            var getName = (Func<int, string>)((num) => $"{baseName} {num}");
            while (me.Any(n => string.Equals(n.Name, getName(++counter), StringComparison.OrdinalIgnoreCase))) ;
            return getName(counter);            
        }
    }
}
