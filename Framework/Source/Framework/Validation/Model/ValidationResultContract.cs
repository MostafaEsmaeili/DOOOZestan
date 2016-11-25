using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.Validation.Model
{
    [DataContract]
    public class ValidationResultContract
    {
        public bool IsValid
        {
            get { return Errors == null || Errors.Count == 0; }
        }

        [DataMember]
        public IList<ValidationFailure> Errors { get; set; }
    }
}
