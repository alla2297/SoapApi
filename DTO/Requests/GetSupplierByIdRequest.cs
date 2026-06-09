using System.Runtime.Serialization;

namespace SoapApi.DTO.Requests
{
    
    [DataContract(Name = "GetSupplierByIdRequest")]
    public class GetSupplierByIdRequest
    {

        [DataMember(Name = "SupplierId", Order = 1)]
        public int SupplierId { get; set; }
    }
}
