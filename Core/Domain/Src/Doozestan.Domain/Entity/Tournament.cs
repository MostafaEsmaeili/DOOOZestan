
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class Tournament
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }

     //   public virtual AspNetUser AspNetUser { get; set; }
      //  public virtual Game Game { get; set; }
    }

}

