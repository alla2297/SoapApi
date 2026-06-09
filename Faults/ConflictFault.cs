using System.Runtime.Serialization;

namespace SoapApi.Faults
{
    [DataContract]
    public class ConflictFault
    {
        [DataMember]
        public string Message { get; set; } = "Conflict detected";
    }
}
