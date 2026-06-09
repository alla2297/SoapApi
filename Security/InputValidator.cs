using System.ServiceModel;
using SoapApi.Faults;

namespace SoapApi.Security;

public static class InputValidator
{
    public static void ValidateNoHtml(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        if (value.Contains("<") || value.Contains(">"))
        {
            throw new FaultException<ValidationFault>(
                new ValidationFault
                {
                    Message = "HTML content is not allowed"
                },
                new FaultReason("Validation error")
            );
        }
    }
}