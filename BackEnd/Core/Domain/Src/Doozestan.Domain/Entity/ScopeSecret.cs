
using Newtonsoft.Json;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ScopeSecret
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public System.DateTimeOffset? Expiration { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int ScopeId { get; set; }

        [JsonIgnore]
        public virtual Scope Scope { get; set; }
    }

}

