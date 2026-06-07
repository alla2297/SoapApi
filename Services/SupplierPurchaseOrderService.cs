using SoapApi.Contracts;
using SoapApi.Data;
using SoapApi.DTO.Requests;
using SoapApi.DTO.Responses;
using SoapApi.Faults;
using SoapApi.Models;
using System.ServiceModel;

namespace SoapApi.Services;

public class SupplierPurchaseOrderService : ISupplierPurchaseOrderService
{
    private readonly AppDbContext _context;
    private readonly AuditService _auditService;
    public SupplierPurchaseOrderService(AppDbContext context, AuditService auditService)
    {
        _context = context;
        _auditService = auditService;
    }

    // --------------------------------------------------------------------------------------
    //     GetSupplierById
    // --------------------------------------------------------------------------------------

    public GetSupplierByIdResponse GetSupplierById(
    GetSupplierByIdRequest request)
    {
        var supplier = _context.Suppliers
            .FirstOrDefault(s => s.SupplierId == request.SupplierId);

        if (supplier == null)
        {
            throw new FaultException<SupplierNotFoundFault>(
                new SupplierNotFoundFault
                {
                    Message = "Supplier not found"
                });
        }
        
        return new GetSupplierByIdResponse
        {
            SupplierId = supplier.SupplierId,
            Name = supplier.Name
        };
    }

    // --------------------------------------------------------------------------------------
    //     GetPurchaseOrderById
    // --------------------------------------------------------------------------------------

    public GetPurchaseOrderByIdResponse GetPurchaseOrderById(
     GetPurchaseOrderByIdRequest request)
    {
        var order = _context.PurchaseOrders
            .FirstOrDefault(p =>
                p.PurchaseOrderId == request.PurchaseOrderId);

        if (order == null)
        {
            throw new FaultException<PurchaseOrderNotFoundFault>(
                new PurchaseOrderNotFoundFault
                {
                    Message = "Purchase order not found"
                });
        }

        return new GetPurchaseOrderByIdResponse
        {
            PurchaseOrderId = order.PurchaseOrderId,
            SupplierId = order.SupplierId,
            TotalAmount = order.TotalAmount,
            Status = order.Status
        };
    }

    // --------------------------------------------------------------------------------------
    //     CreatePurchaseOrder
    // --------------------------------------------------------------------------------------

    public CreatePurchaseOrderResponse CreatePurchaseOrder(
      CreatePurchaseOrderRequest request)
    {
        var supplier = _context.Suppliers
            .FirstOrDefault(s => s.SupplierId == request.SupplierId);

        if (supplier == null)
        {
            throw new FaultException<SupplierNotFoundFault>(
                new SupplierNotFoundFault
                {
                    Message = "Supplier not found"
                });
        }

        var order = new PurchaseOrder
        {
            SupplierId = request.SupplierId,
            TotalAmount = request.TotalAmount,
            Status = "Pending",
            CreatedDate = DateTime.UtcNow
        };

        _context.PurchaseOrders.Add(order);
        _context.SaveChanges();
        _auditService.LogCreatePurchaseOrder(order);


        return new CreatePurchaseOrderResponse
        {
            PurchaseOrderId = order.PurchaseOrderId,
            Message = "Purchase order created successfully"
        };
    }

    // --------------------------------------------------------------------------------------
    //     UpdatePurchaseOrderStatus
    // --------------------------------------------------------------------------------------

    public UpdatePurchaseOrderStatusResponse UpdatePurchaseOrderStatus(
    UpdatePurchaseOrderStatusRequest request)
    {
        var order = _context.PurchaseOrders
            .FirstOrDefault(p =>
                p.PurchaseOrderId == request.PurchaseOrderId
            );

        if (order == null)
        {
            throw new FaultException<PurchaseOrderNotFoundFault>(
                new PurchaseOrderNotFoundFault
                {
                    Message = "Purchase order not found"
                }
            );
        }
        // to auditLog
        String oldStatus = order.Status;
        var validStatuses = new[]
        {
            "Pending",
            "Approved",
            "Shipped",
            "Cancelled"
        };

        if (!validStatuses.Contains(request.Status))
        {
            throw new FaultException<InvalidOrderStatusFault>(
                new InvalidOrderStatusFault
                {
                    Message = "Invalid order status"
                }
            );
        }
       
        order.Status = request.Status;

        _context.SaveChanges();

        _auditService.LogUpdatePurchaseOrderStatus(
            order, oldStatus
        );

        return new UpdatePurchaseOrderStatusResponse
        {
            Message = "Order status updated successfully"
        };
    }
}