using System.Runtime.Serialization;

namespace SoapApi.DTO.Responses;

[DataContract]
public class CreatePurchaseOrderResponse
{
    [DataMember]
    public int PurchaseOrderId { get; set; }

    [DataMember]
    public string Message { get; set; } = string.Empty;
}