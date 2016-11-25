using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Framework.ErrorHandler
{
    public class ErrorHandlerBehavior : IErrorHandler, IServiceBehavior
    {
        //internal static ILog Logger = LogConfigurator.GetCurrentLogger();

        #region IErrorHandler Members

        public bool HandleError(Exception ex)
        {
           // Logger.Error("Application Fault", ex);

            return true;  // error has been handled.
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            //If not already custom fault exception provided
            if (!(error is FaultException))
            {
                var faultDetail = new ApplicationFault { Message = "Server error encountered. All details have been logged." };
                var fex =
                    new FaultException<ApplicationFault>(faultDetail, "Server error encountered.",
                        new FaultCode("ApplicationFault"));
                MessageFault msgFault = fex.CreateMessageFault();

                fault = Message.CreateMessage(version, msgFault, fex.Action);
            }
        }

        #endregion

        #region IServiceBehavior Members

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = channelDispatcherBase as ChannelDispatcher;

                if (channelDispatcher != null)
                {
                    channelDispatcher.ErrorHandlers.Add(this);
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        #endregion
    }
}
