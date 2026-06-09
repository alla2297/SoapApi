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

        if (request.SupplierId <= 0)
        {
            throw new FaultException<ValidationFault>(
                new ValidationFault
                {
                    Message = "Invalid supplier Id"
                },
                new FaultReason("Validation error"));
        }

        var supplier = _context.Suppliers
            .FirstOrDefault(s => s.SupplierId == request.SupplierId);

        if (supplier == null)
        {
            throw new FaultException<NotFoundFault>(
                new NotFoundFault
                {
                    Message = "Supplier not found"
                },
                new FaultReason("Supplier not found"));
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
        if (request.PurchaseOrderId <= 0)
        {
            throw new FaultException<ValidationFault>(
                new ValidationFault
                {
                    Message = "Invalid purchase order Id"
                },
                new FaultReason("Validation error"));
        }
        var order = _context.PurchaseOrders
            .FirstOrDefault(p =>
                p.PurchaseOrderId == request.PurchaseOrderId);

        if (order == null)
        {
            throw new FaultException<NotFoundFault>(
                new NotFoundFault
                {
                    Message = "Purchase order not found"
                },
                new FaultReason("Purchase order not found"));
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
        if (request.SupplierId <= 0)
        {
            throw new FaultException<ValidationFault>(
                new ValidationFault
                {
                    Message = "Invalid supplier Id"
                },
                new FaultReason("Validation error")
            );
        }

        if (request.TotalAmount <= 0)
        {
            throw new FaultException<ValidationFault>(
                new ValidationFault
                {
                    Message = "Invalid TotalAmount"
                },
                new FaultReason("Validation error")
            );
        }

        var supplier = _context.Suppliers
            .FirstOrDefault(s => s.SupplierId == request.SupplierId);

        if (supplier == null)
        {
            throw new FaultException<NotFoundFault>(
                new NotFoundFault
                {
                    Message = "Supplier not found"
                },
                new FaultReason("Supplier not found"));
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
       

        if (string.IsNullOrWhiteSpace(request.Status))
        {
            throw new FaultException<ValidationFault>(
                new ValidationFault
                {
                    Message = "Invalid Status"
                },
                new FaultReason("Validation error")
            );
        }
        
        if (request.PurchaseOrderId <= 0)
        {
            throw new FaultException<ValidationFault>(
                new ValidationFault
                {
                    Message = "Invalid purchase order Id"
                },
                new FaultReason("Validation error")
            );
        }
        var order = _context.PurchaseOrders
            .FirstOrDefault(p =>
                p.PurchaseOrderId == request.PurchaseOrderId
            );

        if (order == null)
        {
            throw new FaultException<NotFoundFault>(
                new NotFoundFault
                {
                    Message = "Purchase order not found"
                },
                new FaultReason("Purchase order not found")
            );
        }
        if (order.Status.Equals(request.Status,StringComparison.OrdinalIgnoreCase))
        {
            throw new FaultException<ConflictFault>(
                new ConflictFault
                {
                    Message = "Order already has this status"
                },
                new FaultReason("Conflict")
            );
        }
        

        string oldStatus = order.Status; // to auditLog
        var validStatuses = new[]
        {
            "Pending",
            "Approved",
            "Shipped",
            "Cancelled"
        };

        if (!validStatuses.Contains(request.Status))
        {
            throw new FaultException<ValidationFault>(
                new ValidationFault
                {
                    Message = "Invalid order status"
                },
                new FaultReason("Validation error")
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