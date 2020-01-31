using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using UnitySample.Common.Entities;
using UnitySample.Common.Exceptions;

namespace UnitySample.Common.Interfaces
{
    /// <summary>
    /// AKV Webservice Interface
    /// </summary>
    [ServiceKnownType(typeof(RandomObject))]
    [ServiceContract(Namespace = "http://schemas.daimler.com/dpe")]
    public interface ITestService
    {
        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        /// <summary>
        /// Finds the software asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<RandomObject> FindRandomStringObjectAsync(int id);

        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        /// <summary>
        /// Finds the software asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> GetObjectStringCount(RandomObject obj);


        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        Task<List<RandomObject>> GetListOfRandomObjects();
    }
}