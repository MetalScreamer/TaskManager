﻿using Jsc.MvvmUtilities;
using Jsc.TaskManager.Models;
using System;

namespace Jsc.TaskManager.ViewModels
{
    public interface INoteViewModel
    {

    }

    public class NoteViewModel : UndoableViewModel, INoteViewModel
    {
        private string text;
        private DateTime date;

        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value, v => text = v); }
        }

        public DateTime DateTime
        {
            get { return date; }
            set { SetProperty(ref date, value, v => date = v); }
        }

        public NoteViewModel(INote note)
        {
            this.Text = note.Text;
            this.DateTime = note.DateTime;
        }
    }
}