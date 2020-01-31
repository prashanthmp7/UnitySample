using System;
using System.ServiceModel;
using System.ServiceProcess;
using UnitySample.Common.Logger;
using UnitySample.TestService.Startup;
using UnitySample.WCF.TestService.Unity;

namespace UnitySample.TestService
{
    /// <summary>
    /// WindowsService für AKV
    /// </summary>
    /// <seealso cref="System.ServiceProcess.ServiceBase" />
    public partial class Service : ServiceBase
    {
        private ServiceHost _svc;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        public Service()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            try
            {

                if (_svc != null)
                {
                    _svc.Close();
                }
                var unityConfig = new UnityConfig();
                _svc = new UnityServiceHost(unityConfig.GetConfiguredContainer(), typeof(UnitySample.WCF.TestService.Implementation.TestService));
                _svc.Open();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                throw;
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            if (_svc != null)
            {
                _svc.Close();
            }
            _svc = null;
        }

        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void StartService(string[] args)
        {
            OnStart(args);
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        public void StopService()
        {
            OnStop();
        }
    }
}
