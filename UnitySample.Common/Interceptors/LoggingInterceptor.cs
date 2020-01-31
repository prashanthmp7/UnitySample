using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.Interception.PolicyInjection.Pipeline;
using UnitySample.Common.Extensions;

namespace UnitySample.Common.Interceptors
{
    /// <summary>
    /// Interceptor für das Log
    /// </summary>
    public class LoggingInterceptor : IDisposable, ICallHandler
    {
        #region Properties (2) 

        /// <summary>
        /// Order in which the handler will be executed
        /// </summary>
        public int Order { get; set; }

        #endregion Properties 

        #region Methods (1) 

        #region Public Methods (1) 

        /// <summary>
        /// Implement this method to execute your handler processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the handler
        /// chain.</param>
        /// <returns>
        /// Return value from the target.
        /// </returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            //if(input.MethodBase.Name.StartsWith("Dispose")
            string name = "unknown User";
            try
            {
                Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                string n = Thread.CurrentPrincipal.Identity.Name;
                name = string.IsNullOrEmpty(n) ? name : n;
            }
            catch (ObjectDisposedException)
            {
                //kann passieren, dann steht weiter unknown user im log
            }

            string param = input.Arguments.Cast<object>()
                .Aggregate("", (current, parameter) => current + (", " + (parameter == null ? "null" : parameter.ToString())));

            if (param.Length > 0) param = param.Substring(2);

            Logger.Logger.Write(string.Format("{0}:LI Enter Method: {1}.{2}({3})", name, input.MethodBase.DeclaringType, input.MethodBase.Name, param), TraceEventType.Verbose);

            IMethodReturn value = getNext()(input, getNext);
            value.InvokeAfterCall(input, d =>
            {
                if (d.Exception != null)
                {
                    Logger.Logger.Write(d.Exception);
                }

                Logger.Logger.Write(string.Format("{0}:LI Exit Method: {1}.{2}({3})", name, input.MethodBase.DeclaringType, input.MethodBase.Name, param), TraceEventType.Verbose);
            });
            return value;
        }

        #endregion Public Methods 

        #endregion Methods 

        #region IDisposable implementation

        // Flag: Has Dispose already been called?
        bool _disposed = false;


        /// Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.                
            }

            // Free any unmanaged objects here.
            //
            _disposed = true;
        }

        #endregion
    }
}
