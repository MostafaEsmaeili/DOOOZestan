using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Framework.Utility;

namespace Doozestan.Common.WcfService.Authorize
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AmberAuth : Attribute, IParameterInspector, IOperationBehavior
    {
        private readonly WcfAuthorizationManager _wcfAuthorizationManager = new WcfAuthorizationManager();
        private string ServiceName { get; set; }
        private string IpAddress { get; set; }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {

        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            try
            {
                BaseRequest request = null;
                if (inputs != null && inputs.Length > 0)
                {
                    var d = SerializeDesrialize.Serialize(inputs[0]);
                    request = inputs[0] as BaseRequest;
                }
                if (request == null)
                {
                    var fc = new BaseResponse { ResponseStatus = ResponseStatus.BadRequest, ResponseMessage = "Bad Request" };
                    throw new FaultException<BaseResponse>(fc);
                }

                OperationContext context = OperationContext.Current;
                if (context == null)
                {

                }
                if (context != null)
                {
                    MessageProperties prop = context.IncomingMessageProperties;
                    var endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    this.IpAddress = endpoint.Address;
                }

                var message = context.RequestContext.RequestMessage;
                string action = context.IncomingMessageHeaders.Action;

                Uri actionUri = new Uri(action);
                var methodName = actionUri.Segments.Last();
                if (!methodName.ToLower().Equals("servielogin"))
                {

                    var res = _wcfAuthorizationManager.CheckAccess(context, request.ServiceUser, request.AuthenticationToken);
                    if (!res)
                    {
                        var fc = new BaseResponse { ResponseStatus = ResponseStatus.MethodNotAllowed, ResponseMessage = "Access Denied!" };
                        throw new FaultException<BaseResponse>(fc);
                    }
                    return true;
                }
                else
                {
                    var fc = new BaseResponse { ResponseStatus = ResponseStatus.MethodNotAllowed, ResponseMessage = "Access Denied!" };
                    throw new FaultException<BaseResponse>(fc);
                }
            }
            catch (Exception )
            {
                var fc = new BaseResponse { ResponseStatus = ResponseStatus.MethodNotAllowed, ResponseMessage = "Access Denied!" };
                throw new FaultException<BaseResponse>(fc);
            }
            return true;
        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            ServiceName = dispatchOperation.Parent.Type.Name;
            dispatchOperation.ParameterInspectors.Add(this);
        }

        public void Validate(OperationDescription operationDescription)
        {

        }
    }
}
