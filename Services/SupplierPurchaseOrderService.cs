using SoapApi.Contracts;

namespace SoapApi.Services;

public class SupplierPurchaseOrderService : ISupplierPurchaseOrderService
{
    public GetSupplierByIdResponse GetSupplierById(GetSupplierByIdRequest request)
    {
        return new GetSupplierByIdResponse
        {
            SupplierId = request.SupplierId,
            Name = "Test Supplier"
        };
    }

    public GetPurchaseOrderByIdResponse GetPurchaseOrderById(
        GetPurchaseOrderByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public CreatePurchaseOrderResponse CreatePurchaseOrder(
        CreatePurchaseOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public UpdatePurchaseOrderStatusResponse UpdatePurchaseOrderStatus(
        UpdatePurchaseOrderStatusRequest request)
    {
        throw new NotImplementedException();
    }
}