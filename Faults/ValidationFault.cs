using System.Runtime.Serialization;

namespace SoapApi.Faults
{
    [DataContract]
    public class ValidationFault
    {
        [DataMember]
        public string Message { get; set; } = "Validation Error";
    }
}
