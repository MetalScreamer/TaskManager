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
        IEnumerable<MenuItem> ContextMenu { get; }
    }

    public class NoteListViewModel : ViewModelBase, INoteListViewModel
    {
        private INoteViewModel selected;
        private IContentManager contentManager;
        private IEnumerable<INoteViewModel> initialNotes;

        public ObservableCollection<INoteViewModel> Notes { get; } = new ObservableCollection<INoteViewModel>();
        public ObservableCollection<MenuItem> ContextMenu { get; } = new ObservableCollection<MenuItem>();

        public DelegateCommand Add { get; }
        public DelegateCommand Remove { get; }

        public INoteViewModel Selected
        {
            get { return selected; }
            set
            {
                SetProperty(ref selected, value);
                Remove.RaiseCanExecuteChanged();
            }
        }

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

        public NoteListViewModel(
            IContentManager contentManager,
            IEnumerable<INoteViewModel> initialNotes,
            Func<IContentManager, INoteViewModel> noteFactory)
        {
            this.contentManager = contentManager;
            this.initialNotes = initialNotes;

            Add = new DelegateCommand(_ => AddNote(() => noteFactory(contentManager)));
            Remove = new DelegateCommand(_ => RemoveNote(), _ => CanRemoveNote());

            ContextMenu.Add(new MenuItem() { Text = "Edit Note", Command = new DelegateCommand(_ => EditNote(contentManager)) });
        }

        private void EditNote(IContentManager contentManager)
        {
            contentManager.Load(Selected);
        }

        private bool CanRemoveNote()
        {
            return Selected != null;
        }

        private void RemoveNote()
        {
            Notes.Remove(Selected);
        }

        private void AddNote(Func<INoteViewModel> noteFactory)
        {
            var newNote = noteFactory();
            newNote.DateTime = DateTime.Now;
            Notes.Add(newNote);
        }
    }
}
