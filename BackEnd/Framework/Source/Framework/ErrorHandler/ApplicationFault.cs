using System.Runtime.Serialization;

namespace Framework.ErrorHandler
{
    [DataContract]
    public class ApplicationFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}
