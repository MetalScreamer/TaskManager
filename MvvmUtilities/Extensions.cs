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
            var counter = 1;
            string result;

            do
            {
                result = $"{baseName} {counter}";
                counter++;
            } while (me.Any(n => String.Equals(n.Name, result, StringComparison.OrdinalIgnoreCase)));

            return result;
        }
    }
}
