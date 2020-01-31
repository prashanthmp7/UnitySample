using CommonServiceLocator;
using System.Runtime.ExceptionServices;
using System.Windows.Input;
using Unity.Interception.Interceptors.TypeInterceptors.VirtualMethodInterception;
using Unity;
using Unity.Interception;
using UnitySample.Common.Logger;
using System.Threading.Tasks;
using System;
using System.IO;
using Prism.Regions;
using System.Windows;
using Prism.Modularity;
using Prism.Ioc;
using Prism.Events;
using Unity.Interception.ContainerIntegration;
using UnitySample.Common.Interceptors;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Threading;
using System.Security.Permissions;
using Prism.Mvvm;
using Prism.Unity;
using UnitySample.Common.Interfaces;
using UnitySample.Core.Interception;
using UnitySample.WCF.TestService.Implementation;
using Unity.Lifetime;
using UnitySample.Core;
using static UnitySample.Common.Consts.Consts;
using UnitySample.Common.Consts;
using Unity.Injection;

namespace UnitySample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        /// <summary>
        /// Programmstart
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }


        /// <summary>
        /// Erzeugt eine neue Shell
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            Logger.Write("Start Bootstrapper", TraceEventType.Information);


            ViewModelLocationProvider.SetDefaultViewModelFactory(t =>
            {
                return Container.Resolve(t);
            });
            MainWindow view = Container.GetContainer().Resolve<MainWindow>();
            return view;
        }

        //
        // Summary:
        //     Initializes the modules.
        protected override void InitializeModules()
        {
            base.InitializeModules();
        }

        //
        // Summary:
        //     Contains actions that should occur last.
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }


        /// <summary>
        /// Initialize the shell
        /// </summary>
        /// <param name="shell"></param>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);

            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Current.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            EventManager.RegisterClassHandler(typeof(Window), Window.PreviewMouseDownEvent, new MouseButtonEventHandler(OnPreviewMouseDown));

            Application.Current.MainWindow = (Window)shell;
            ShowMainWindow();


        }

        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {

            }
            catch (Exception err)
            {

            }
        }

        /// <summary>
        /// Lädt und konfiguriert den ModuleCatalog
        /// </summary>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        /// <summary>
        /// Lädt und konfiguriert den Container
        /// </summary>
        //
        // Summary:
        //     Used to register types with the container that will be used by your application.
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            try
            {
                containerRegistry.GetContainer().AddExtension(new Interception());
                containerRegistry.RegisterSingleton(typeof(IEventAggregator), typeof(EventAggregator));
                containerRegistry.Register<object, MainWindow>("MainWindow");

                containerRegistry.GetContainer().RegisterType(typeof(MainWindowViewModel), typeof(MainWindowViewModel), null, new ContainerControlledLifetimeManager(),
                    new Interceptor<VirtualMethodInterceptor>(),
                    new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                    new AdditionalInterface<INotifyPropertyChanged>(),
                    new InterceptionBehavior<NotifyPropertyChangedBehavior>());

                RegisterServiceRequests(containerRegistry);

            }
            catch (Exception ex)
            {

            }
        }
        private void RegisterServiceRequests(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer().RegisterType(typeof(ITestService), typeof(TestService), null, new TransientLifetimeManager(),
                    new Interceptor<VirtualMethodInterceptor>(),
                    new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                    new InterceptionBehavior<OperationContextScopeHandler>());

            containerRegistry.Register<ITestServiceRequests, TestServiceOnlineRequests>(OnlineMode.Online.ToString());
            Container.GetContainer().RegisterType<Func<OnlineMode, ITestServiceRequests>>(
               new InjectionFactory(c => new Func<OnlineMode, ITestServiceRequests>(
                   d => c.Resolve<ITestServiceRequests>(d.ToString()))));
        }

        private void ShowMainWindow()
        {
            Logger.Write("End of Bootstrapper", TraceEventType.Information);
        }

        private void CurrentDomain_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            // <see cref="http://stackoverflow.com/questions/1127431/xmlserializer-giving-filenotfoundexception-at-constructor"/>
            if (e.Exception is FileNotFoundException && e.Exception.Message.Contains("XmlSerializers"))
            {
                return;
            }

            TaskCanceledException ex = e.Exception as TaskCanceledException;

            if (ex?.CancellationToken != null && ex.CancellationToken.IsCancellationRequested)
            {
                return;
            }

            //Common.Logger.Logger.Write(e.Exception);
        }

        [HandleProcessCorruptedStateExceptions]
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Write(e.ExceptionObject as Exception);
        }
        
        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
        }
    }
}