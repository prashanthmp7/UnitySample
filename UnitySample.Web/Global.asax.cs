using CommonServiceLocator;
using Couchbase;
using DCX.DPE.WCF.Unity;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace UnitySample.Web
{
    /// <summary>
    /// Global.asax
    /// </summary>
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var unityConfig = new UnityConfig();           
            AreaRegistration.RegisterAllAreas();
            var webApiConfig = new WebApiConfig(unityConfig.GetConfiguredContainer());
            GlobalConfiguration.Configure(webApiConfig.Register);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}