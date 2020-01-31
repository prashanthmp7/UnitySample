using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitySample.WCF.TestService.Behaviors
{
    /// <summary>
    /// Wird nach der Bereinigung des Services aufgerufen. Hier können Referenzen freigegeben werden
    /// </summary>
    public interface IReleasableBehavior
    {
        /// <summary>
        /// Releases the instance.
        /// </summary>
        void ReleaseInstance();
    }
}
