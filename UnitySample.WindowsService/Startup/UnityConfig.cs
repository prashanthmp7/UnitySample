using CommonServiceLocator;
using Prism.Events;
using System;
using System.Diagnostics;
using Unity;
using Unity.Interception;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.TypeInterceptors.VirtualMethodInterception;
using Unity.Lifetime;
using UnitySample.Common.Core;
using UnitySample.Common.Entities;
using UnitySample.Common.Interceptors;
using UnitySample.Core.Implementations;
using UnitySample.Core.Interception;
using UnitySample.WCF.TestService.Unity;
using static UnitySample.Common.Consts.Consts;

namespace UnitySample.TestService.Startup
{
    /// <summary>
    ///
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            UnityContainer container = new UnityContainer();
            RegisterTypes(container);
            UnityServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
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

        /// <summary>
        /// Registers the types.
        /// </summary>
        /// <param name="container">The container.</param>
        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance(new ServiceStartupType(OnlineMode.Online), new ContainerControlledLifetimeManager());
            container.RegisterServiceRequests();

            container.RegisterInstance<IEventAggregator>(new EventAggregator(), new ContainerControlledLifetimeManager());

            container.AddNewExtension<Interception>();
            container.RegisterType<Service>();

            container.RegisterType<UnitySample.WCF.TestService.Implementation.TestService>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new InterceptionBehavior<OperationContextScopeHandler>());


            container.RegisterType<RandomObject>();
            container.Resolve<RandomObject>();
        }
    }
}