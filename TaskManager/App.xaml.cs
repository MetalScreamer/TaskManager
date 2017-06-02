using Jsc.MvvmUtilities;
using Jsc.TaskManager.DAL;
using Jsc.TaskManager.Models;
using Jsc.TaskManager.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var vm = container.Resolve<IMainWindowViewModel>();
            var win = new MainWindow(vm);
            var context = container.Resolve<TaskManagerDbContext>();

            var jobs = new List<IJobViewModel>();
            foreach (var job in context.Jobs)
            {
                jobs.Add(container.Resolve<IJobViewModel>(
                    new ParameterOverride("contentManager", vm),
                    new ParameterOverride("job", job)));
            }

            vm.Content = container.Resolve<IJobListViewModel>(new ParameterOverride("contentManager", vm), new ParameterOverride("jobs", jobs));

            win.Show();
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container
                .RegisterType<IJob, Job>()
                .RegisterType<ITask, Task>()
                .RegisterType<INote, Note>()
                .RegisterType<IMainWindowViewModel, MainWindowViewModel>()
                .RegisterType<IJobListViewModel, JobListViewModel>()
                .RegisterType<IJobViewModel, JobViewModel>()
                .RegisterType<ITaskListViewModel, TaskListViewModel>()
                .RegisterType<ITaskViewModel, TaskViewModel>()
                .RegisterType<INoteListViewModel, NoteListViewModel>()
                .RegisterType<INoteViewModel, NoteViewModel>()
                //Create an instance of the dal
                .RegisterInstance(new TaskManagerDbContext())
                .RegisterInstance<IDataAccess<IJob>>(container.Resolve<TaskManagerDbContext>())
                .RegisterInstance<IDataAccess<ITask>>(container.Resolve<TaskManagerDbContext>())
                .RegisterInstance<IDataAccess<INote>>(container.Resolve<TaskManagerDbContext>())
                .RegisterInstance<Func<IContentManager, IJobViewModel>>(cm => container.Resolve<IJobViewModel>(new ParameterOverride("contentManager", cm)))
                .RegisterInstance<Func<IContentManager, ITask, ITaskViewModel>>((cm, t) => container.Resolve<ITaskViewModel>(new ParameterOverride("contentManager", cm), new ParameterOverride("task", t)))
                .RegisterInstance<Func<IContentManager, ITaskViewModel>>(cm => container.Resolve<ITaskViewModel>(new ParameterOverride("contentManager", cm)))
                .RegisterInstance<Func<IContentManager, INote, INoteViewModel>>((cm, n) => container.Resolve<INoteViewModel>(new ParameterOverride("contentManager", cm), new ParameterOverride("note", n)))
                .RegisterInstance<Func<IContentManager, INoteViewModel>>((cm) => container.Resolve<INoteViewModel>(new ParameterOverride("contentManager", cm)))
                .RegisterInstance<Func<IContentManager, IJobViewModel, INoteListViewModel>>(
                    (cm, jvm) =>
                    {
                        var noteFactory = container.Resolve<Func<IContentManager, INote, INoteViewModel>>();
                        var notes = jvm.Job.Notes.Select(n => noteFactory(cm, n));

                        return container.Resolve<INoteListViewModel>(new ParameterOverride("contentManager", cm), new ParameterOverride("initialNotes", notes));
                    })
                .RegisterInstance<Func<IContentManager, ITaskViewModel, INoteListViewModel>>(
                    (cm, tvm) =>
                    {
                        var noteFactory = container.Resolve<Func<IContentManager, INote, INoteViewModel>>();
                        var notes = tvm.Task.Notes.Select(n => noteFactory(cm, n));

                        return container.Resolve<INoteListViewModel>(new ParameterOverride("contentManager", cm), new ParameterOverride("initialNotes", notes));
                    })
                .RegisterInstance<Func<IContentManager, IJobViewModel, ITaskListViewModel>>(
                    (cm, jvm) =>
                    {
                        var taskFactory = container.Resolve<Func<IContentManager, ITask, ITaskViewModel>>();
                        var tasks = jvm.Job.Tasks.Select(t => taskFactory(cm, t));

                        return container.Resolve<ITaskListViewModel>(new ParameterOverride("contentManager", cm), new ParameterOverride("initialTasks", tasks));
                    })
                .RegisterInstance<Func<IContentManager, ITaskViewModel, ITaskListViewModel>>(
                    (cm, jvm) =>
                    {
                        var taskFactory = container.Resolve<Func<IContentManager, ITask, ITaskViewModel>>();
                        var tasks = jvm.Task.Children.Select(c => taskFactory(cm, c));

                        return container.Resolve<ITaskListViewModel>(new ParameterOverride("contentManager", cm), new ParameterOverride("initialTasks", tasks));
                    });
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            container.Dispose();
        }
    }
}
