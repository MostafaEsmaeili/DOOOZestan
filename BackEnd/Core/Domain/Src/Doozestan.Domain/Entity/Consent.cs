
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class Consent
    {
        public string Subject { get; set; }
        public string ClientId { get; set; }
        public string Scopes { get; set; }
    }

}

