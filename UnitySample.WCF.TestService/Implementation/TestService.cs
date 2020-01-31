using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UnitySample.Common.Entities;
using UnitySample.Common.Interceptors;
using UnitySample.Common.Interfaces;

namespace UnitySample.WCF.TestService.Implementation
{
    /// <summary>
    /// AKV Webservice Implementierung
    /// </summary>
    [Log]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true)]
    public class TestService : IDisposable, ITestService
    {
        public virtual async Task<RandomObject> FindRandomStringObjectAsync(int id)
        {
            List<RandomObject> random = new List<RandomObject>();

            for (int i = 0; i < 1000; i++)
            {
                RandomObject randomObj = new RandomObject() { Id = i, RandomObjectInstance = "Instance -" + i };
                await randomObj.GetObjectStringCount();
                random.Add(randomObj);
            }

            var result = random.Where(x => x.Id == id).ToList().FirstOrDefault();
            return result;
        }

        public virtual async Task<List<RandomObject>> GetListOfRandomObjects()
        {
            List<RandomObject> randomList = new List<RandomObject>();

            for (int i = 0; i < 1000; i++)
            {
                RandomObject randomObj = new RandomObject() { Id = i, RandomObjectInstance = "Instance -" + i };
                randomList.Add(randomObj);
            }
            return randomList;
        }


        public virtual async Task<int> GetObjectStringCount(RandomObject obj)
        {
            await Task.Delay(10);
            return obj.RandomObjectInstance.Length;
        }


        #region IDispose implementation

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
