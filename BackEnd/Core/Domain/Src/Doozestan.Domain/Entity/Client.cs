
using Newtonsoft.Json;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class Client
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public int Flow { get; set; }
        public bool AllowClientCredentialsOnly { get; set; }
        public string LogoutUri { get; set; }
        public bool LogoutSessionRequired { get; set; }
        public bool RequireSignOutPrompt { get; set; }
        public bool AllowAccessToAllScopes { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public int RefreshTokenUsage { get; set; }
        public bool UpdateAccessTokenOnRefresh { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public int AccessTokenType { get; set; }
        public bool EnableLocalLogin { get; set; }
        public bool IncludeJwtId { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public bool PrefixClientClaims { get; set; }
        public bool AllowAccessToAllGrantTypes { get; set; }

        [JsonIgnore]
        public virtual System.Collections.Generic.ICollection<ClientClaim> ClientClaims { get; set; }
        [JsonIgnore]
        public virtual System.Collections.Generic.ICollection<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        [JsonIgnore]
        public virtual System.Collections.Generic.ICollection<ClientCustomGrantType> ClientCustomGrantTypes { get; set; }
        [JsonIgnore]
        public virtual System.Collections.Generic.ICollection<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
        [JsonIgnore]
        public virtual System.Collections.Generic.ICollection<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        [JsonIgnore]
        public virtual System.Collections.Generic.ICollection<ClientRedirectUri> ClientRedirectUris { get; set; }
        [JsonIgnore]
        public virtual System.Collections.Generic.ICollection<ClientScope> ClientScopes { get; set; }
        [JsonIgnore]
        public virtual System.Collections.Generic.ICollection<ClientSecret> ClientSecrets { get; set; }

        public Client()
        {
            ClientClaims = new System.Collections.Generic.List<ClientClaim>();
            ClientCorsOrigins = new System.Collections.Generic.List<ClientCorsOrigin>();
            ClientCustomGrantTypes = new System.Collections.Generic.List<ClientCustomGrantType>();
            ClientIdPRestrictions = new System.Collections.Generic.List<ClientIdPRestriction>();
            ClientPostLogoutRedirectUris = new System.Collections.Generic.List<ClientPostLogoutRedirectUri>();
            ClientRedirectUris = new System.Collections.Generic.List<ClientRedirectUri>();
            ClientScopes = new System.Collections.Generic.List<ClientScope>();
            ClientSecrets = new System.Collections.Generic.List<ClientSecret>();
        }
    }

}

