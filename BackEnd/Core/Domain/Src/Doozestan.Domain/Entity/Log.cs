
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class Log
    {
        public int Id { get; set; }
        public System.DateTime EventDateTime { get; set; }
        public string EventLevel { get; set; }
        public string UserName { get; set; }
        public string MachineName { get; set; }
        public string EventMessage { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorClass { get; set; }
        public string ErrorMethod { get; set; }
        public string InnerErrorMessage { get; set; }
    }

}

