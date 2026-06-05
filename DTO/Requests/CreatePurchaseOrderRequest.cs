using System.Runtime.Serialization;

namespace SoapApi.DTO.Requests;

[DataContract]
public class CreatePurchaseOrderRequest
{
    [DataMember]
    public int SupplierId { get; set; }

    [DataMember]
    public decimal TotalAmount { get; set; }
}