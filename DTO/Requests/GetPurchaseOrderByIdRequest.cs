using System.Runtime.Serialization;

namespace SoapApi.DTO.Requests;

[DataContract(Name = "GetPurchaseOrderByIdRequest")]
public class GetPurchaseOrderByIdRequest
{
    [DataMember(Name = "PurchaseOrderId", Order = 1)]
    public int PurchaseOrderId { get; set; }
}