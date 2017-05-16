using Jsc.MvvmUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.ViewModels
{
    public class TestVm
    {
        private int counter = 1;

        public Delegatecommand Btn { get; }

        public TestVm()
        {
            Btn = new Delegatecommand(_ => Debug.WriteLine($"Clicked {counter++}"));
        }
    }
}
