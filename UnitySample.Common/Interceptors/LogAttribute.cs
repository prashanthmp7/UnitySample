using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.PolicyInjection.Policies;

namespace UnitySample.Common.Interceptors
{
    /// <summary>
    /// HandlerAttribute für LoggingInterceptor
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Property)]
    public class LogAttribute : HandlerAttribute, IDisposable
    {
        private LoggingInterceptor _loggingInterceptor;

        /// <summary>
        /// Derived classes implement this method. When called, it
        /// creates a new call handler as specified in the attribute
        /// configuration.
        /// </summary>
        /// <param name="container">The <see cref="T:Microsoft.Practices.Unity.IUnityContainer" /> to use when creating handlers,
        /// if necessary.</param>
        /// <returns>
        /// A new call handler object.
        /// </returns>
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            _loggingInterceptor = new LoggingInterceptor();
            return _loggingInterceptor;
        }

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
                _loggingInterceptor.Dispose();
                _loggingInterceptor = null;
            }

            // Free any unmanaged objects here.
            //
            _disposed = true;
        }

        #endregion
    }
}
