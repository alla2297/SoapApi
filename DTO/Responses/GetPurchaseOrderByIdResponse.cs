using System.Runtime.Serialization;

namespace SoapApi.DTO.Responses;

[DataContract]
public class GetPurchaseOrderByIdResponse
{
    [DataMember]
    public int PurchaseOrderId { get; set; }

    [DataMember]
    public int SupplierId { get; set; }

    [DataMember]
    public decimal TotalAmount { get; set; }

    [DataMember]
    public string Status { get; set; } = string.Empty;
}