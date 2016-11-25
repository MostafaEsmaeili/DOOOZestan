using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace Framework.Validation.Service
{
    public class ServiceValidationElement : BehaviorExtensionElement
    {
        private const string EnabledAttributeName = "enabled";

        [ConfigurationProperty(EnabledAttributeName, DefaultValue = true, IsRequired = false)]
        public bool Enabled
        {
            get { return (bool)base[EnabledAttributeName]; }
            set { base[EnabledAttributeName] = value; }
        }

        protected override object CreateBehavior()
        {
            return new ServiceValidationBehavior(Enabled);
        }

        public override Type BehaviorType
        {
            get { return typeof(ServiceValidationBehavior); }
        }
    }
}
