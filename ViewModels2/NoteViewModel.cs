﻿using Jsc.MvvmUtilities;
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
        INote Note { get; }
        string Text { get; set; }
        DateTime DateTime { get; }
        bool IsNew { get; }
        bool IsEditable { get; }
        bool IsLocked { get; set; }
        void Save();
        void Remove();
    }

    public class NoteViewModel : UndoableViewModel, INoteViewModel
    {
        private string text;
        private DateTime date;
        private string displayDate;
        private string displayTime;
        private IContentManager contentManager;
        private IStorage<INote> noteStorage;
        private string displayDateAndTime;
        private bool isNew = true;
        private bool isLocked = true;
        private bool isEditable;

        public INote Note { get; }

        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value, v => text = v); }
        }

        public DateTime DateTime
        {
            get { return date; }
            private set
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

        public string DisplayDateAndTime
        {
            get { return displayDateAndTime; }
            private set { SetProperty(ref displayDateAndTime, value); }
        }

        public bool IsNew
        {
            get { return isNew; }
            private set
            {
                if(SetProperty(ref isNew, value))
                {
                    SetIsEditable();
                }
            }
        }

        public bool IsEditable
        {
            get { return isEditable; }
            private set { SetProperty(ref isEditable, value); }
        }

        public bool IsLocked
        {
            get { return isLocked; }
            set
            {
                if(SetProperty(ref isLocked, value))
                {
                    SetIsEditable();
                }
            }
        }       

        public DelegateCommand OkCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public NoteViewModel(
            IContentManager contentManager, 
            INote note,
            IStorage<INote> noteStorage)
        {
            Note = note;
            IsNew = note.IsNew;
            IsLocked = !IsNew;
            DateTime = DateTime.Now;
            this.noteStorage = noteStorage;
            LoadFromNote(note);
            this.contentManager = contentManager;

            OkCommand = new DelegateCommand(_ => DoOk());
            CancelCommand = new DelegateCommand(_ => DoCancel());            
        }

        private void SetIsEditable()
        {
            IsEditable = IsNew || !IsLocked;
        }

        private void LoadFromNote(INote note)
        {
            Text = note.Text;
            DateTime = note.DateTime == DateTime.MinValue ? DateTime.Now : note.DateTime;
        }

        private void WriteToNote(INote note)
        {
            note.Text = Text;
            note.DateTime = DateTime;            
        }

        private void DoCancel()
        {
            LoadFromNote(Note);
            contentManager.Unload(this);
        }

        private void DoOk()
        {
            Save();
            contentManager.Unload(this);
        }

        private void SetDisplayDateAndTime()
        {
            DisplayDate = DateTime.ToString("MM/dd/yyyy");
            DisplayTime = DateTime.ToString("hh:mm:ss tt");
            DisplayDateAndTime = $"{DisplayDate} {DisplayTime}";
            RaisePropertyChanged(nameof(DisplayDateAndTime));
        }

        public void Save()
        {
            WriteToNote(Note);
            noteStorage.Save(Note);
            noteStorage.Commit();
            IsNew = false;
            IsLocked = true;
        }

        public void Remove()
        {
            noteStorage.Remove(Note);
            noteStorage.Commit();
        }
    }
}
