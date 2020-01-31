using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitySample.Common.Entities;
using UnitySample.Common.Interceptors;
using UnitySample.Common.Interfaces;
using UnitySample.Common.ServiceClient;
using static UnitySample.Common.Consts.Consts;

namespace UnitySample.Core
{
    [Log]
    /// <summary>
    /// Online Requests for Software Service
    /// </summary>
    /// <seealso cref="DCX.DPE.Common.Interfaces.WCFServices.ISoftwareService" />
    public class TestServiceClientRequest : TestServiceOnlineRequests
    {
        private readonly ITestServiceRequests _offlineTestServiceRequests;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftwareServiceOnlineRequests" /> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="serviceRequestFactory">The service request factory.</param>
        public TestServiceClientRequest(
            Func<OnlineMode, ITestServiceRequests> serviceRequestFactory)
        {
            _offlineTestServiceRequests = serviceRequestFactory(OnlineMode.Offline);
        }

        /// <summary>
        /// Finds the software asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="deepness">if set to <c>true</c> [with children].</param>
        /// <returns></returns>
        public override async Task<RandomObject> FindRandomStringObjectAsync(int id)
        {
            var obj = await new ServiceClient<ITestService>()
                .ExecuteAsync(d => d.FindRandomStringObjectAsync(id));

            return obj;
        }


        /// <summary>
        /// Finds the software asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="deepness">if set to <c>true</c> [with children].</param>
        /// <returns></returns>
        public override async Task<int> GetObjectStringCount(RandomObject rObj)
        {
            var obj = await new ServiceClient<ITestService>()
                .ExecuteAsync(d => d.GetObjectStringCount(rObj));

            return obj;
        }


        /// <summary>
        /// Finds the software asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="deepness">if set to <c>true</c> [with children].</param>
        /// <returns></returns>
        public override async Task<List<RandomObject>> GetListOfRandomObjects()
        {
            var obj = await new ServiceClient<ITestService>()
                .ExecuteAsync(d => d.GetListOfRandomObjects());

            return obj;
        }
    }
}
