using Jsc.MvvmUtilities;

namespace Jsc.TaskManager.ViewModels
{
    public class TaskViewModel : UndoableViewModel
    {
        private string testProp1;
        private string testProp2;

        public string TestProp1
        {
            get { return testProp1; }
            set
            {                
                SetProperty(ref testProp1, value, v => testProp1 = v);
            }
        }

        public string TestProp2
        {
            get { return testProp2; }
            set
            {
                SetProperty(ref testProp2, value, v => testProp2 = v);
            }
        }
    }
}