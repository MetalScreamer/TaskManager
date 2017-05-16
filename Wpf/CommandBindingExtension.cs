using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Jsc.Wpf
{
    public class CommandBindingExtension : Binding
    {
        public CommandBindingExtension(string path) : base(path)
        {
            Converter = new CommandConverter();
        }
    }
}
