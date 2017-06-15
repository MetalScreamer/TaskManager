using Jsc.MvvmUtilities;
using Jsc.TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.ViewModels
{
    public interface IJobViewModel : IHasName
    {
        //IJob Job { get; }
        string Description { get; set; }

        INoteListViewModel Notes { get; }
        ITaskListViewModel Tasks { get; }

        ICommand CancelCommand { get; }
        ICommand OkCommand { get; }

        void Save();
        void Remove();
    }

    public class JobViewModel : UndoableViewModel, IJobViewModel
    {
        private string description;
        private string name;
        private IContentManager contentManager;
        //private IRepository<IJob> dal;

        //public IJob Job { get; }

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value, v => description = v); }
        }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value, v => name = v); }
        }

        public INoteListViewModel Notes { get; }
        public ITaskListViewModel Tasks { get; }

        public DelegateCommand CancelCommand { get; }
        public DelegateCommand OkCommand { get; }

        ICommand IJobViewModel.CancelCommand
        {
            get { return CancelCommand; }
        }

        ICommand IJobViewModel.OkCommand
        {
            get { return OkCommand; }
        }

        public JobViewModel(
            IContentManager contentManager, 
            //IJob job,
            Func<IContentManager, IJobViewModel, INoteListViewModel> noteListFactory,
            Func<IContentManager, IJobViewModel, ITaskListViewModel> taskListFactory/*,
            IRepository<IJob> dal*/)
        {
            this.contentManager = contentManager;
            //this.dal = dal;

            //Job = job;
            //LoadFromJob(job);

            Notes = noteListFactory(contentManager, this);
            Tasks = taskListFactory(contentManager, this);

            //Tasks.TaskAddedCallback = TaskAdded;
            //Notes.NoteAddedCallback = NoteAdded;

            OkCommand = new DelegateCommand(_ => DoOk());
            CancelCommand = new DelegateCommand(_ => DoCancel());
        }

        //private void NoteAdded(INoteViewModel noteVm)
        //{
        //    Job.AddNote(noteVm.Note);
        //}

        //private void TaskAdded(ITaskViewModel taskVm)
        //{
        //    Job.AddTask(taskVm.Task);
        //}

        private void DoCancel()
        {
            //LoadFromJob(Job);
            contentManager.Unload(this);
        }

        private void DoOk()
        {
            Save();

            contentManager.Unload(this);
        }

        public void Remove()
        {
            //dal.Remove(Job);
            //dal.Commit();
        }

        public void Save()
        {
            //WriteToModel(Job);
            //dal.Save(Job);
            //dal.Commit();
        }

        //private void WriteToModel(IJob job)
        //{
        //    job.Name = Name;
        //    job.Description = Description;
        //}

        //private void LoadFromJob(IJob job)
        //{
        //    Name = job.Name;
        //    Description = job.Description;
        //}
    }
}
