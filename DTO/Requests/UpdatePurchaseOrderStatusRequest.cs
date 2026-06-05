using System.Runtime.Serialization;

namespace SoapApi.DTO.Requests;

[DataContract]
public class UpdatePurchaseOrderStatusRequest
{
    [DataMember]
    public int PurchaseOrderId { get; set; }

    [DataMember]
    public string Status { get; set; } = string.Empty;
}