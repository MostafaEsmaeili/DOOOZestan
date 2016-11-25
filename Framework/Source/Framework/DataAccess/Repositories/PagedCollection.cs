using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.DataAccess
{
    [DataContract]
    public class PagedCollection<T>
    {
        [DataMember]
        public List<T> Result { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }
    }
}
