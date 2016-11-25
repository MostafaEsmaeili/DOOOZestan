using System;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Framework.Authorization;

namespace Doozestan.Common.WcfService
{
    public class WcfAuthorizationManager
    {
        private IAuthorizationProvider<ServiceAuthorizationContext> _authorizationProvider;
        public IAuthorizationProvider<ServiceAuthorizationContext> AuthorizationProvider
        {
            get { return _authorizationProvider ?? (_authorizationProvider = new WcfAuthorizationProvider()); }
            set { _authorizationProvider = value; }
        }

        protected bool CheckAccessCore(OperationContext operationContext, string username, string token)
        {
            try
            {
                string ip = "";
                //AspNetCache cache = Utility.Cache.AspNetCache;

                MessageProperties prop = operationContext.IncomingMessageProperties;
                var endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                if (endpoint != null)
                {
                    ip = endpoint.Address;
                }

                var message = operationContext.RequestContext.RequestMessage;
                string action = operationContext.IncomingMessageHeaders.Action;

                Uri actionUri = new Uri(action);
                var methodName = actionUri.Segments.Last();
                if (!methodName.ToLower().Equals("servielogin"))
                {
                    //string token = "";

                    var encoding = Encoding.UTF8;
                    var encryptedKey = TripleDESProvider.Encrypt(token);
                    var keyByte = encoding.GetBytes(encryptedKey);
                    var hmacsha512 = new HMACSHA512(keyByte);
                    var hashmessage = hmacsha512.ComputeHash(keyByte);
                    var enckey = Convert.ToBase64String(hashmessage);


                    var login = WcfAutenticationProvider.Get();
                    if (login.TockenKey == enckey && login.UserName == username)
                    {
                        //TWO Hours
                        if ((DateTime.Now - login.LastSet).Minutes > 120)
                        {
                            return false;
                        }
                        return true;
                        //TODO: add CheckAccess
                        //return CheckAccess(operationContext, "", actionUri, ip, "");
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected bool CheckAccess(OperationContext operationContext, string userName, Uri uri, string ip, string token)
        {
            var authContext = new ServiceAuthorizationContext
            {
                RequestUri = uri,
                UserName = userName,
                IP = ip,
                Token = token,
                LastSet = DateTime.UtcNow
            };

            return AuthorizationProvider.CheckAccess(authContext);
            //return false;
        }



        public bool CheckAccess(OperationContext context, string userName, string token)
        {
            return CheckAccessCore(context, userName, token);
        }
    }
}
