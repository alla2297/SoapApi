using System.Runtime.Serialization;

namespace SoapApi.Faults;

[DataContract]
public class AuthenticationFault
{
    [DataMember]
    public string Message { get; set; } = "Authentication failed";
}