using System;
using System.Threading.Tasks;
using UnitySample.Common.ServiceClient;
using UnitySample.Common.Interfaces;
using System.Collections.Generic;
using UnitySample.Common.Entities;

namespace UnitySample.Core
{
    public class TestServiceOnlineRequests : IDisposable, ITestServiceRequests
    {
        /// <summary>
        /// Finds the software asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="deepness">if set to <c>true</c> [with children].</param>
        /// <returns></returns>
        public virtual async Task<RandomObject> FindRandomStringObjectAsync(int id)
        {
            return await new ServiceClient<ITestService>()
                .ExecuteAsync(d => d.FindRandomStringObjectAsync(id));
        }

        /// <summary>
        /// Finds the software asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="deepness">if set to <c>true</c> [with children].</param>
        /// <returns></returns>
        public virtual async Task<int> GetObjectStringCount(RandomObject obj)
        {
            return await new ServiceClient<ITestService>()
                .ExecuteAsync(d => d.GetObjectStringCount(obj));
        }

        /// <summary>
        /// Finds the software asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="deepness">if set to <c>true</c> [with children].</param>
        /// <returns></returns>
        public virtual async Task<List<RandomObject>> GetListOfRandomObjects()
        {
            return await new ServiceClient<ITestService>()
                .ExecuteAsync(d => d.GetListOfRandomObjects());
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}