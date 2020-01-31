using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitySample.Common.Entities;

namespace UnitySample.Common.Interfaces
{
    public interface ITestServiceRequests : ITestService
    {
        /// <summary>
        /// Finds the software asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<RandomObject> FindRandomStringObjectAsync(int id);

        /// <summary>
        /// Finds the software asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> GetObjectStringCount(RandomObject obj);


        Task<List<RandomObject>> GetListOfRandomObjects();
    }
}
