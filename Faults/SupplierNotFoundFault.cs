using System.Runtime.Serialization;

namespace SoapApi.Faults
{
    [DataContract]
    public class SupplierNotFoundFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}