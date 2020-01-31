using DCX.DPE.WCF.Unity;
using System.Web.Http;
using Unity;

namespace UnitySample.Web
{
    /// <summary>
    /// Konfiguration der WebAPI
    /// </summary>
    public class WebApiConfig
    {
        IUnityContainer _unityContainer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unityContainer"></param>
        public WebApiConfig(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new UnityResolver(_unityContainer);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
