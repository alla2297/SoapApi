using System.ServiceModel;
using SoapApi.Faults;

namespace SoapApi.Security;

public static class SecurityValidator
{
    private const string ApiKey = "WAREHOUSE-SECRET-KEY";

    public static void ValidateApiKey(string? apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new FaultException<AuthenticationFault>(
                new AuthenticationFault
                {
                    Message = "API Key is required"
                },
                new FaultReason("Authentication failed")
            );
        }

        if (apiKey != ApiKey)
        {
            throw new FaultException<AuthenticationFault>(
                new AuthenticationFault
                {
                    Message = "Invalid API Key"
                },
                new FaultReason("Authentication failed")
            );
        }
    }
}