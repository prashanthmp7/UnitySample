using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitySample.Common.Interceptors;
using UnitySample.Common.Interfaces;

namespace UnitySample.Common.Entities
{
    [Log]
    [Serializable]
    public class RandomObject 
    {
        public virtual int Id { get; set; }
        public virtual string RandomObjectInstance { get; set; }

        public virtual async Task<int> GetObjectStringCount()
        {
            await Task.Delay(1);
            return this.RandomObjectInstance.Length;
        }
    }
}
