using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;
using UnitySample.WCF.TestService.Unity;

namespace UnitySample.Core.HostFactory
{
    /// <summary>
    /// ServiceHostFactory für Unity
    /// </summary>
    public class UnityServiceHostFactory : ServiceHostFactory
    {
        /// <summary>
        /// Creates a <see cref="T:System.ServiceModel.ServiceHost" /> for a specified type of service with a specific base address.
        /// </summary>
        /// <param name="serviceType">Specifies the type of service to host.</param>
        /// <param name="baseAddresses">The <see cref="T:System.Array" /> of type <see cref="T:System.Uri" /> that contains the base addresses for the service hosted.</param>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.ServiceHost" /> for the type of service specified with a specific base address.
        /// </returns>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            IUnityContainer container = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUnityContainer)) as IUnityContainer;
            return new UnityServiceHost(container, serviceType, baseAddresses);
        }
    }
}
