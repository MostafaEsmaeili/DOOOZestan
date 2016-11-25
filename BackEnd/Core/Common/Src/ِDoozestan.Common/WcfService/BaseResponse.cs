using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Doozestan.Common.WcfService
{
    [DataContract]
    public class BaseResponse
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
        
        [DataMember]
        public string ResponseMessage { get; set; }

        //public List<Framework.Utility.Message> ErrorMessageList { get; set; }
        [DataMember]
        public List<string> ErrorList { get; set; }
        //[DataMember]
        //public Exception Exception { get; set; }
    }
}
