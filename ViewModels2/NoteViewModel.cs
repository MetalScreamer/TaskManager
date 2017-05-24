using Jsc.MvvmUtilities;
using Jsc.TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.ViewModels
{
    public interface INoteViewModel
    {
        string Text { get; set; }
        DateTime DateTime { get; set; }
    }

    public class NoteViewModel : UndoableViewModel, INoteViewModel
    {
        private string text;
        private DateTime date;
        private string displayDate;
        private string displayTime;
        private IContentManager contentManager;

        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value, v => text = v); }
        }

        public DateTime DateTime
        {
            get { return date; }
            set
            {
                SetProperty(ref date, value, v => date = v);
                SetDisplayDateAndTime();
            }            
        }

        public string DisplayDate
        {
            get { return displayDate; }
            private set { SetProperty(ref displayDate, value); }
        }

        public string DisplayTime
        {
            get { return displayTime; }
            private set { SetProperty(ref displayTime, value); }
        }

        public string DisplayDateAndTime { get; private set; }

        public DelegateCommand OkCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public NoteViewModel(IContentManager contentManager, INote note)
        {
            Text = note.Text;
            DateTime = note.DateTime;
            this.contentManager = contentManager;

            OkCommand = new DelegateCommand(_ => DoOk());
            CancelCommand = new DelegateCommand(_ => DoCancel());
        }

        private void DoCancel()
        {
            contentManager.Unload(this);
        }

        private void DoOk()
        {
            contentManager.Unload(this);
        }

        private void SetDisplayDateAndTime()
        {
            DisplayDate = DateTime.ToString("MM/dd/yyyy");
            DisplayTime = DateTime.ToString("hh:mm:ss tt");
            DisplayDateAndTime = $"{DisplayDate} {DisplayTime}";
            RaisePropertyChanged(nameof(DisplayDateAndTime));
        }
    }
}
