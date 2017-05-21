using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.MvvmUtilities
{
    public class MenuItem
    {
        public string Text { get; set; }
        public DelegateCommand Command { get; set; }
        public object CommandParamerter { get; set; }
        public bool IsSeperator { get; set; }

        public ObservableCollection<MenuItem> Children { get; } = new ObservableCollection<MenuItem>();
    }
}
