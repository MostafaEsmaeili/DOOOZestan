using System.Runtime.Serialization;

namespace Framework.Validation.Model
{
    [DataContract]
    public class ValidationFailure
    {
        /// <summary>
        /// The name of the property.
        /// </summary>
        [DataMember]
        public string MemberName { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        [DataMember]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Creates a textual representation of the failure.
        /// </summary>
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}
