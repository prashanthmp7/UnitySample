using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;
using UnitySample.Common.Consts;
using System.ServiceModel;

namespace UnitySample.Core.Interception
{
    /// <summary>
    /// Workaround for issue in .NET Framework (OperationContext.Current is null after first await when using async/await in WCF service)
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior" />
    public class OperationContextScopeHandler : IDisposable, IInterceptionBehavior
    {
        /// <summary>
        /// Implement this method to execute your behavior processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the behavior chain.</param>
        /// <returns>
        /// Return value from the target.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (CallContext.LogicalGetData(Consts.ServiceSecurityContext) == null && OperationContext.Current != null)
            {
                CallContext.LogicalSetData(Consts.ServiceSecurityContext, OperationContext.Current.ServiceSecurityContext);
            }

            return getNext()(input, getNext);
        }

        /// <summary>
        /// Returns the interfaces required by the behavior for the objects it intercepts.
        /// </summary>
        /// <returns>
        /// The required interfaces.
        /// </returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// Returns a flag indicating if this behavior will actually do anything when invoked.
        /// </summary>
        /// <remarks>
        /// This is used to optimize interception. If the behaviors won't actually
        /// do anything (for example, PIAB where no policies match) then the interception
        /// mechanism can be skipped completely.
        /// </remarks>
        public bool WillExecute => true;

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
                //
            }

            // Free any unmanaged objects here.
            //
            _disposed = true;
        }

        #endregion
    }
}
