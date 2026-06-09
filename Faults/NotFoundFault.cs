using System.Runtime.Serialization;

namespace SoapApi.Faults
{
    [DataContract]
    public class NotFoundFault
    {
        [DataMember]
        public required string Message { get; set; } = "Not Found Error";
    }
}
