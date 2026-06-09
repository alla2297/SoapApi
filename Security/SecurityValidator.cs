using DotNetEnv;
using SoapApi.Faults;
using System.ServiceModel;

namespace SoapApi.Security;

public static class SecurityValidator
{
    public static void ValidateToken(string? token)
    {
        Env.Load();
        var expectedToken =
            Environment.GetEnvironmentVariable(
                "SOAP_API_KEY");

        //Console.WriteLine("TOKEN:");
        //Console.WriteLine(token);

        //Console.WriteLine("EXPECTED:");
        //Console.WriteLine(expectedToken);

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new FaultException<AuthenticationFault>(
                new AuthenticationFault
                {
                    Message = "Access token is required"
                },
                new FaultReason("Authentication failed")
            );
        }

        if (token != expectedToken)
        {
            throw new FaultException<AuthenticationFault>(
                new AuthenticationFault
                {
                    Message = "Invalid access token"
                },
                new FaultReason("Authentication failed")
            );
        }
    }
}