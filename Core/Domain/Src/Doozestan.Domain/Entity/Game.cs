
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

      //  public virtual System.Collections.Generic.ICollection<Tournament> Tournaments { get; set; }

     //   public virtual AspNetUser AspNetUser { get; set; }

        public Game()
        {
           // Tournaments = new System.Collections.Generic.List<Tournament>();
        }
    }

}

