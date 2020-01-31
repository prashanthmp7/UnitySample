using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using UnitySample.Common.Interfaces;
using static UnitySample.Common.Consts.Consts;

namespace UnitySample.Core.Implementations
{
    public static class Extensions
    {

        /// <summary>
        /// Registers the service requests.
        /// </summary>
        /// <param name="container">The container.</param>
        public static void RegisterServiceRequests(this IUnityContainer container)
        {
            container.RegisterType<ITestServiceRequests, TestServiceOnlineRequests>(OnlineMode.Online.ToString());
            container.RegisterType<Func<OnlineMode, ITestServiceRequests>>(
                new InjectionFactory(c => new Func<OnlineMode, ITestServiceRequests>(
                    d => c.Resolve<ITestServiceRequests>(d.ToString()))));
            
        }
    }
}
