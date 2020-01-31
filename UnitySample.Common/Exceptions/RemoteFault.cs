using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnitySample.Common.Consts;

namespace UnitySample.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class RemoteFault
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteFault"/> class.
        /// </summary>
        public RemoteFault()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteFault"/> class.
        /// </summary>
        /// <param name="faultDescription">The fault discription.</param>
        /// <param name="faultId">The fault identifier.</param>
        public RemoteFault(String faultDescription, Consts.Consts.FaultExceptionEnum faultId)
        {
            FaultDescription = faultDescription;
            FaultId = faultId;
        }

        /// <summary>
        /// Gets or sets the fault description.
        /// </summary>
        /// <value>
        /// The fault description.
        /// </value>
        [DataMember]
        public String FaultDescription { get; set; }

        /// <summary>
        /// Gets or sets the fault identifier.
        /// </summary>
        /// <value>
        /// The fault identifier.
        /// </value>
        [DataMember]
        public Consts.Consts.FaultExceptionEnum FaultId { get; set; }
    }
}
