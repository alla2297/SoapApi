using System.Runtime.Serialization;

namespace SoapApi.DTO.Requests
{
    [DataContract]
    public class GetSupplierByIdRequest
    {
        [DataMember]
        public int SupplierId { get; set; }
    }
}
