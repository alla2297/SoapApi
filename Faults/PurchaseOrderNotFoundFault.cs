using System.Runtime.Serialization;

namespace SoapApi.Faults;

[DataContract]
public class PurchaseOrderNotFoundFault
{
    [DataMember]
    public string Message { get; set; } = string.Empty;
}