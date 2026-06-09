using SoapApi.DTO.Requests;
using SoapApi.DTO.Responses;
using SoapApi.Faults;
using SoapApi.Services;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SoapApi.Contracts
{
    [ServiceContract(Namespace = "http://library.soapapi.org/")]
    public interface ISupplierPurchaseOrderService
    {
        // READ
        [OperationContract]
        [FaultContract(typeof(AuthenticationFault))]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(ValidationFault))]
        GetSupplierByIdResponse GetSupplierById(GetSupplierByIdRequest request);

        [OperationContract]
        [FaultContract(typeof(AuthenticationFault))]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(ValidationFault))]
        GetPurchaseOrderByIdResponse GetPurchaseOrderById(GetPurchaseOrderByIdRequest request);
        
        // CHANGES
        [OperationContract]
        [FaultContract(typeof(AuthenticationFault))]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(ValidationFault))]
        CreatePurchaseOrderResponse CreatePurchaseOrder(CreatePurchaseOrderRequest request);

        [OperationContract]
        [FaultContract(typeof(AuthenticationFault))]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ConflictFault))]
        UpdatePurchaseOrderStatusResponse UpdatePurchaseOrderStatus(UpdatePurchaseOrderStatusRequest request);
    }
}
