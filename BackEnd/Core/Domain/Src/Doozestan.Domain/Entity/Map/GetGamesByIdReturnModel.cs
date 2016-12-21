
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class GetGamesByIdReturnModel
    {
        public System.Int32 Id { get; set; }
        public System.String Title { get; set; }
        public System.DateTime? StartDate { get; set; }
        public System.DateTime? EndDate { get; set; }
        public System.Int32? Status { get; set; }
        public System.String WinnerId { get; set; }
    }																 

}

