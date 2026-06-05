using System.Runtime.Serialization;

namespace SoapApi.Faults;

[DataContract]
public class InvalidOrderStatusFault
{
    [DataMember]
    public string Message { get; set; } = string.Empty;
}