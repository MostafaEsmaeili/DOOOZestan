using System.ServiceModel.Configuration;

namespace Framework.Authorization
{
    public class AuthorizationBehaviorExtensionElement: BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new AuthorizationEndPointBehavior();
        }

        public override System.Type BehaviorType
        {
            get { return typeof (AuthorizationEndPointBehavior); }
        }
    }
}