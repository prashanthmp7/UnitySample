using CommonServiceLocator;
using DCX.DPE.Common;
using DCX.DPE.Common.Constants;
using DCX.DPE.Common.Core;
using DCX.DPE.Common.DataTransferObjects;
using DCX.DPE.Common.Interceptors;
using DCX.DPE.Common.Interceptors.Helper;
using DCX.DPE.Domain.Core.Implementations;
using DCX.DPE.Domain.Core.Interception;
using DCX.DPE.Domain.Core.Interfaces.Entities;
using DCX.DPE.WCF.Authorization;
using DCX.DPE.WCF.Implementations;
using DCX.DPE.WCF.Unity;
using DCX.DPE.Web.Controllers;
using Prism.Events;
using System;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Services.Description;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Interception;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.TypeInterceptors.VirtualMethodInterception;
using Unity.Lifetime;

namespace UnitySample.Web
{
    /// <summary>
    /// Konfiguration für den IoC Container Unity
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        internal readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.RegisterInstance(typeof(HttpConfiguration), GlobalConfiguration.Configuration);
            UnityServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
            RegisterTypes(container);
            //GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        #endregion Unity Container
        
        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<OnlineMode>(new ContainerControlledLifetimeManager(), new InjectionFactory((c) => { return OnlineMode.Online; }));

            container.RegisterInstance(new ServiceStartupType(OnlineMode.Online), new ContainerControlledLifetimeManager());
            container.RegisterServiceRequests();

            container.RegisterInstance<IEventAggregator>(new EventAggregator(), new ContainerControlledLifetimeManager());

            container.AddNewExtension<Interception>();
            container.RegisterType<Service>();
            container.RegisterType<DDManController>();
            container.Configure<Interception>()
                .SetInterceptorFor<DDManController>(new VirtualMethodInterceptor());

            container.RegisterType<UserService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<GroupService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());


            container.RegisterType<ChangeLogService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<MasterDataService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<RoleService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<UserSettingService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<ProjectService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<CriteriaService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<CriterionSpecificationService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<LabelClusterService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<FunktionClusterService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<AKVSpecificationService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<TicketService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<UserImportMapService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());

            container.RegisterType<IUserLocator, UserLocator>();
            container.Configure<Interception>()
                .SetInterceptorFor<UserLocator>(new VirtualMethodInterceptor());

            container.RegisterType<Domain.UserModule.Module>();
            container.Resolve<Domain.UserModule.Module>().Initialize();

            container.RegisterType<Domain.SoftwareManagement.Module>();
            container.Resolve<Domain.SoftwareManagement.Module>().Initialize();

            container.RegisterType<Domain.Repository.Module>();
            container.Resolve<Domain.Repository.Module>().Initialize();

            container.RegisterType<Domain.MasterDataModule.Module>();
            container.Resolve<Domain.MasterDataModule.Module>().Initialize();

            container.RegisterType<Domain.Cluster.Module>();
            container.Resolve<Domain.Cluster.Module>().Initialize();

            container.RegisterType<Domain.TicketModule.Module>();
            container.Resolve<Domain.TicketModule.Module>().Initialize();

            container.RegisterType<Domain.PinboardModule.Module>();
            container.Resolve<Domain.PinboardModule.Module>().Initialize();

            container.RegisterType<Domain.QgroupModule.Module>();
            container.Resolve<Domain.QgroupModule.Module>().Initialize();

            container.RegisterType(typeof(EntityChangeMonitor<>));

            container.RegisterType<OpenItemEventDispatcher>(new InjectionConstructor(
                 new ResolvedParameter<IEventAggregator>() ,
                new ResolvedParameter<GenericDomainMapper<IUser, UserDto>>()));
            container.Configure<Interception>()
                .SetInterceptorFor<OpenItemEventDispatcher>(new VirtualMethodInterceptor());

            //If started from client these classes are not needed
            ServiceStartupType containerStartupType = container.Resolve<ServiceStartupType>();
            if (containerStartupType.OnlineMode == OnlineMode.Online)
                container.RegisterInstance(container.Resolve<OpenItemEventDispatcher>(), new ContainerControlledLifetimeManager());

        }
    }
}