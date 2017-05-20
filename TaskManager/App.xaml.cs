using Jsc.TaskManager.DAL;
using Jsc.TaskManager.Models;
using Jsc.TaskManager.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Windows;

namespace Jsc.TaskManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private UnityContainer container;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            container = new UnityContainer();
            TaskManagerDbContext.Initialize();
            RegisterTypes(container);
            var win = new MainWindow(container.Resolve<IJobListViewModel>());
            win.Show();
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container
                .RegisterType<IJob, Job>()
                .RegisterType<ITask, Task>()
                .RegisterType<INote, Note>()
                .RegisterType<ITaskManagerDbContext, TaskManagerDbContext>()
                .RegisterType<IJobListViewModel, JobListViewModel>()
                .RegisterType<IJobViewModel, JobViewModel>()
                .RegisterType<ITaskViewModel, TaskViewModel>()
                .RegisterType<INoteViewModel, NoteViewModel>()
                .RegisterInstance<Func<IJob, IJobViewModel>>(j => container.Resolve<IJobViewModel>(new ParameterOverride("job", j)))
                .RegisterInstance<Func<ITask, ITaskViewModel>>(t => container.Resolve<ITaskViewModel>(new ParameterOverride("task", t)))
                .RegisterInstance<Func<INote, INoteViewModel>>(n => container.Resolve<INoteViewModel>(new ParameterOverride("note", n)))
                ;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            container.Dispose();
        }
    }
}
