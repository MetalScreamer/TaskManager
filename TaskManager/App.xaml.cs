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
            var vm = container.Resolve<IMainWindowViewModel>();
            var win = new MainWindow(vm);
            vm.Content = container.Resolve<IJobListViewModel>(new ParameterOverride("contentManager", vm));    
                    
            win.Show();
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container
                .RegisterType<IJob, Job>()
                .RegisterType<ITask, Task>()
                .RegisterType<INote, Note>()
                .RegisterType<ITaskManagerDbContext, TaskManagerDbContext>()
                .RegisterType<IMainWindowViewModel, MainWindowViewModel>()
                .RegisterType<IJobListViewModel, JobListViewModel>()
                .RegisterType<IJobViewModel, JobViewModel>()
                .RegisterType<ITaskViewModel, TaskViewModel>()
                .RegisterType<INoteViewModel, NoteViewModel>()
                .RegisterInstance<Func<IContentManager, IJob, IJobViewModel>>((cm, j) => container.Resolve<IJobViewModel>(new ParameterOverride("contentManager", cm), new ParameterOverride("job", j)))
                .RegisterInstance<Func<IContentManager, IJobViewModel>>(cm => container.Resolve<IJobViewModel>(new ParameterOverride("contentManager", cm)))
                .RegisterInstance<Func<IContentManager, ITask, ITaskViewModel>>((cm, t) => container.Resolve<ITaskViewModel>(new ParameterOverride("contentManager", cm), new ParameterOverride("task", t)))
                .RegisterInstance<Func<IContentManager, ITaskViewModel>>(cm => container.Resolve<ITaskViewModel>(new ParameterOverride("contentManager", cm)))
                .RegisterInstance<Func<IContentManager, INote, INoteViewModel>>((cm, n) => container.Resolve<INoteViewModel>(new ParameterOverride("contentManager", cm), new ParameterOverride("note", n)))
                .RegisterInstance<Func<IContentManager, INoteViewModel>>((cm) => container.Resolve<INoteViewModel>(new ParameterOverride("contentManager", cm)))
                ;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            container.Dispose();
        }
    }
}
