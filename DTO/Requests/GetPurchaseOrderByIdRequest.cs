using System.Runtime.Serialization;

namespace SoapApi.DTO.Requests;

[DataContract]
public class GetPurchaseOrderByIdRequest
{
    [DataMember]
    public int PurchaseOrderId { get; set; }
}