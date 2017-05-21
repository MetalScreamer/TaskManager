using Jsc.MvvmUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.ViewModels
{
    public interface IMainWindowViewModel : IContentManager
    {
        object Content { get; set; }

        DelegateCommand Previous { get; }
        DelegateCommand Next { get; }
    }

    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private object content;

        private Stack<object> previousStack = new Stack<object>();
        private Stack<object> nextStack = new Stack<object>();

        public object Content
        {
            get { return content; }
            set
            {
                SetContent(value, true, previousStack);
            }
        }

        public DelegateCommand Previous { get; }
        public DelegateCommand Next { get; }

        public MainWindowViewModel()
        {
            Previous = new DelegateCommand(_ => DoPrevious(), _ => CanDoPrevious());
            Next = new DelegateCommand(_ => DoNext(), _ => CanDoNext());
        }

        private bool CanDoNext()
        {
            return nextStack.Count > 0;
        }

        private void DoNext()
        {
            SetContent(nextStack.Pop(), false, previousStack);
        }

        private bool CanDoPrevious()
        {
            return previousStack.Count < 0;
        }

        private void DoPrevious()
        {
            SetContent(previousStack.Pop(), false, nextStack);
        }

        private void SetContent(object newContent, bool clearNext, Stack<object> pushTo)
        {
            var current = Content;
            if (SetProperty(ref content, newContent, nameof(Content)) && current != null)
            {
                pushTo?.Push(current);
                if(clearNext) nextStack.Clear();
                Next.RaiseCanExecuteChanged();
                Previous.RaiseCanExecuteChanged();
            }
        }

        void IContentManager.LoadContent(object content)
        {
            Content = content;
        }

        void IContentManager.Unload(object content)
        {
            if (Content == content)
            {
                object newContent = null;
                if (previousStack.Count > 0) newContent = previousStack.Pop();
                SetContent(newContent, false, null); 
            }
        }
    }
}
