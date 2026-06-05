using System.Runtime.Serialization;

namespace SoapApi.DTO.Responses
{
    [DataContract]
    public class GetSupplierByIdResponse
    {
        [DataMember]
        public int SupplierId { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
