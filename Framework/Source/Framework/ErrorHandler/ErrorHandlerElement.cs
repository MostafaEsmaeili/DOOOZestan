using System;
using System.ServiceModel.Configuration;

namespace Framework.ErrorHandler
{
    public class ErrorHandlerElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new ErrorHandlerBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(ErrorHandlerBehavior);
            }
        }
    }
}
