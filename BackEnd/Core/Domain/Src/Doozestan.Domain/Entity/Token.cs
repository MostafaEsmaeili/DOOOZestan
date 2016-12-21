
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class Token
    {
        public string Key { get; set; }
        public short TokenType { get; set; }
        public string SubjectId { get; set; }
        public string ClientId { get; set; }
        public string JsonCode { get; set; }
        public System.DateTimeOffset Expiry { get; set; }
    }

}

