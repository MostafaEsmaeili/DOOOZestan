
using Newtonsoft.Json;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ClientCustomGrantType
    {
        public int Id { get; set; }
        public string GrantType { get; set; }
        public int ClientId { get; set; }

        [JsonIgnore]
        public virtual Client Client { get; set; }
    }

}

