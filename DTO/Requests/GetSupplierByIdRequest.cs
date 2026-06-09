using System.Runtime.Serialization;

namespace SoapApi.DTO.Requests
{
    
    [DataContract(Name = "GetSupplierByIdRequest")]
    public class GetSupplierByIdRequest
    {
        [DataMember(Name = "AccessToken", Order = 0)]
        public string AccessToken { get; set; } = string.Empty;

        [DataMember(Name = "SupplierId", Order = 1)]
        public int SupplierId { get; set; }
    }
}
