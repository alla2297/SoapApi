using System.Runtime.Serialization;

namespace SoapApi.Faults
{
    [DataContract]
    public class SupplierNotFoundFault
    {
        [DataMember]
        public required string Message { get; set; }
    }
}