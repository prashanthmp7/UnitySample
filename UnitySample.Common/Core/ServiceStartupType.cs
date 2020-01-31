using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnitySample.Common.Consts.Consts;

namespace UnitySample.Common.Core
{
    public class ServiceStartupType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceStartupType"/> class.
        /// </summary>
        /// <param name="onlineMode">The online mode.</param>
        public ServiceStartupType(OnlineMode onlineMode)
        {
            OnlineMode = onlineMode;
        }

        /// <summary>
        /// Gets the online mode.
        /// </summary>
        /// <value>
        /// The online mode.
        /// </value>
        public OnlineMode OnlineMode { get; private set; }
    }
}
