
using Newtonsoft.Json;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class Scope
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public int Type { get; set; }
        public bool IncludeAllClaimsForUser { get; set; }
        public string ClaimsRule { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public bool AllowUnrestrictedIntrospection { get; set; }

        [JsonIgnore]
        public virtual System.Collections.Generic.ICollection<ScopeClaim> ScopeClaims { get; set; }
        [JsonIgnore]
        public virtual System.Collections.Generic.ICollection<ScopeSecret> ScopeSecrets { get; set; }

        public Scope()
        {
            ScopeClaims = new System.Collections.Generic.List<ScopeClaim>();
            ScopeSecrets = new System.Collections.Generic.List<ScopeSecret>();
        }
    }

}

