using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitySample.Common.Interceptors;

namespace UnitySample.Common.Interfaces
{
    [Log]
    public interface IRandomObject
    {
        int Id { get; set; }
        string RandomObjectInstance { get; set; }

        Task<int> GetObjectStringCount();
    }
}
