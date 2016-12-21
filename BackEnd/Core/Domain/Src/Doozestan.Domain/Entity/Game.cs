
using Newtonsoft.Json;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public System.DateTime? StartDate { get; set; }
        public System.DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public string WinnerId { get; set; }

        [JsonIgnore]
        public virtual System.Collections.Generic.ICollection<Tournament> Tournaments { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Game()
        {
            Tournaments = new System.Collections.Generic.List<Tournament>();
        }
    }

}

