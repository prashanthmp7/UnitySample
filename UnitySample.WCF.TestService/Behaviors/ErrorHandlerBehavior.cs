using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace UnitySample.WCF.TestService.Behaviors
{
    /// <summary>
    /// ErrorHandlerBehavior
    /// </summary>
    public class ErrorHandlerBehavior : BehaviorExtensionElement
    {
        /// <summary>
        /// Creates a behavior extension based on the current configuration settings.
        /// </summary>
        /// <returns>
        /// The behavior extension.
        /// </returns>
        protected override object CreateBehavior()
        {
            return new ErrorServiceBehavior();

        }

        /// <summary>
        /// Gets the type of behavior.
        /// </summary>
        public override Type BehaviorType
        {

            get { return typeof(ErrorServiceBehavior); }
        }
    }
}
