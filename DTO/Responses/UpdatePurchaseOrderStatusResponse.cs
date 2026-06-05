using System.Runtime.Serialization;

namespace SoapApi.DTO.Responses;

[DataContract]
public class UpdatePurchaseOrderStatusResponse
{
    [DataMember]
    public string Message { get; set; } = string.Empty;
}