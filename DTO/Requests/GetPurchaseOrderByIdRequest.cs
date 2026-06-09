using System.Runtime.Serialization;

namespace SoapApi.DTO.Requests;

[DataContract(Name = "GetPurchaseOrderByIdRequest")]
public class GetPurchaseOrderByIdRequest
{
    [DataMember(Name = "AccessToken", Order = 0)]
    public string AccessToken { get; set; } = string.Empty;

    [DataMember(Name = "PurchaseOrderId", Order = 1)]
    public int PurchaseOrderId { get; set; }
}