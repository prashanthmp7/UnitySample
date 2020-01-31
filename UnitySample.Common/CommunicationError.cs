using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitySample.Common
{
    /// <summary>
    /// PubSubEvent for CommunicationException
    /// </summary>
    /// <seealso cref="Prism.Events.PubSubEvent{Exception}" />
    public class CommunicationError : PubSubEvent<Exception>
    { }

}
