using Jsc.MvvmUtilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.ViewModels
{
    public interface INoteListViewModel
    {
        INoteViewModel Selected { get; set; }
        IEnumerable<INoteViewModel> Notes { get; }
        ICommand Add { get; }
        ICommand Remove { get; }
        ICommand EditNote { get; }
        IEnumerable<MenuItem> ContextMenu { get; }
        Action<INoteViewModel> NoteAddedCallback { get; set; }
    }

    public class NoteListViewModel : ViewModelBase, INoteListViewModel
    {
        private INoteViewModel selected;
        private IContentManager contentManager;

        public ObservableCollection<INoteViewModel> Notes { get; } = new ObservableCollection<INoteViewModel>();
        public ObservableCollection<MenuItem> ContextMenu { get; } = new ObservableCollection<MenuItem>();

        public DelegateCommand Add { get; }
        public DelegateCommand Remove { get; }
        public DelegateCommand EditNote { get; }

        public INoteViewModel Selected
        {
            get { return selected; }
            set
            {
                SetProperty(ref selected, value);
                Remove.RaiseCanExecuteChanged();
            }
        }

        public Action<INoteViewModel> NoteAddedCallback { get; set; }

        IEnumerable<INoteViewModel> INoteListViewModel.Notes
        {
            get { return Notes; }
        }

        ICommand INoteListViewModel.Add
        {
            get { return Add; }
        }

        ICommand INoteListViewModel.Remove
        {
            get { return Remove; }
        }

        IEnumerable<MenuItem> INoteListViewModel.ContextMenu
        {
            get { return ContextMenu; }
        }

        ICommand INoteListViewModel.EditNote
        {
            get { return EditNote; }
        }

        public NoteListViewModel(
            IContentManager contentManager,
            IEnumerable<INoteViewModel> initialNotes,
            Func<IContentManager, INoteViewModel> noteFactory)
        {
            this.contentManager = contentManager;

            foreach (var note in initialNotes)
            {
                Notes.Add(note);
            }

            Add = new DelegateCommand(_ => AddNote(() => noteFactory(contentManager)));
            Remove = new DelegateCommand(_ => RemoveNote(), _ => CanRemoveNote());
            EditNote = new DelegateCommand(_ => DoEditNote(contentManager));

            ContextMenu.Add(new MenuItem() { Text = "Edit Note", Command = EditNote });
        }

        private void DoEditNote(IContentManager contentManager)
        {
            contentManager.Load(Selected);
        }

        private bool CanRemoveNote()
        {
            return Selected != null;
        }

        private void RemoveNote()
        {
            var noteVm = Selected;
            noteVm.Remove();
            Notes.Remove(noteVm);
        }

        private void AddNote(Func<INoteViewModel> noteFactory)
        {
            var newNote = noteFactory();
            Notes.Add(newNote);
            NoteAddedCallback?.Invoke(newNote);
            Selected = newNote;
        }
    }
}
