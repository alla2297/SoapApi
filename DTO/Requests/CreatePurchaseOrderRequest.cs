using System.Runtime.Serialization;

namespace SoapApi.DTO.Requests;

[DataContract(Name = "CreatePurchaseOrderRequest")]
public class CreatePurchaseOrderRequest
{
    [DataMember(Name = "SupplierId", Order = 1)]
    public int SupplierId { get; set; }

    [DataMember(Name = "TotalAmount", Order = 2)]
    public decimal TotalAmount { get; set; }
}