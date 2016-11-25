using System;

namespace Framework.Authorization
{
    public class ServiceAuthorizationContext
    {
        public Uri RequestUri { get; set; }
        public string UserName { get; set; }
        public string IP { get; set; }
        public string Token { get; set; }
        public DateTime LastSet { get; set; }
        
    }
}
