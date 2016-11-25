using System;

namespace Framework.Authorization
{
    public class TokenManager
    {
        internal static Guid Token;
        
        public static void SetToken(Guid token)
        {
            TokenManager.Token = token;
        }
    }
}