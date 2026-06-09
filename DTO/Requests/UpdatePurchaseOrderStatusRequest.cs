using System.Runtime.Serialization;

namespace SoapApi.DTO.Requests;

[DataContract(Name = "UpdatePurchaseOrderStatusRequest")]
public class UpdatePurchaseOrderStatusRequest
{

    [DataMember(Name = "PurchaseOrderId", Order = 1)]
    public int PurchaseOrderId { get; set; }

    [DataMember(Name = "Status", Order = 2)]
    public string Status { get; set; } = string.Empty;
}