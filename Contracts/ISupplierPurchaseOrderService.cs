using System.Runtime.Serialization;
using System.ServiceModel;

namespace SoapApi.Contracts
{
    [ServiceContract(Namespace = "http://library.soapapi.org/")]
    public interface ISupplierPurchaseOrderService
    {
        // READ
        [OperationContract]
        [FaultContract(typeof(SupplierNotFoundFault))]
        GetSupplierByIdResponse GetSupplierById(GetSupplierByIdRequest request);

        [OperationContract]
        [FaultContract(typeof(PurchaseOrderNotFoundFault))]
        GetPurchaseOrderByIdResponse GetPurchaseOrderById(GetPurchaseOrderByIdRequest request);
        
        // CHANGES
        [OperationContract]
        [FaultContract(typeof(SupplierNotFoundFault))]
        CreatePurchaseOrderResponse CreatePurchaseOrder(CreatePurchaseOrderRequest request);

        [OperationContract]
        [FaultContract(typeof(PurchaseOrderNotFoundFault))]
        [FaultContract(typeof(InvalidOrderStatusFault))]
        UpdatePurchaseOrderStatusResponse UpdatePurchaseOrderStatus(UpdatePurchaseOrderStatusRequest request);
    }
}
