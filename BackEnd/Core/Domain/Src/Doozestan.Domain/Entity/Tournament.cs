
using Newtonsoft.Json;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class Tournament
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public virtual Game Game { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

}

