using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Framework.Authorization
{
    public class ClientAuthorizationMessageInspector: IClientMessageInspector
    {
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (!string.IsNullOrEmpty(TokenManager.Token.ToString()))
            {
                var tokenHeader = new MessageHeader<string>
                {
                    Content = TokenManager.Token.ToString(),
                    Actor = "",
                    MustUnderstand = false
                };
                request.Headers.Add(tokenHeader.GetUntypedHeader("token", "urn:asset-com:services:token-header:2014-11"));
            } 
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }
    }
}