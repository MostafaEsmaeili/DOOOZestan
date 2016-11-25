using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Framework.Validation.Service
{
    public class ServiceValidationBehavior : IEndpointBehavior
    {
        public bool Enabled { get; set; }

        public ServiceValidationBehavior(bool enabled)
        {
            Enabled = enabled;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            if (!Enabled)
                return;

            foreach (ClientOperation clientOperation in clientRuntime.Operations)
            {
                // clientOperation.ParameterInspectors.Add(new ServiceValidationParameterInspector { ValidatorProvider = new ServiceValidatorProvider { ValidatorFactory = new IoCValidatorFactory() } });
            }
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            if (!Enabled)
                return;

            foreach (DispatchOperation dispatchOperation in endpointDispatcher.DispatchRuntime.Operations)
            {
                //  dispatchOperation.ParameterInspectors.Add(new ServiceValidationParameterInspector { ValidatorProvider = new ServiceValidatorProvider { ValidatorFactory = new IoCValidatorFactory() } });
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
