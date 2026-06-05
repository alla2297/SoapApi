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
    public SupplierPurchaseOrderService(AppDbContext context)
    {
        _context = context;
    }

    public GetSupplierByIdResponse GetSupplierById(
    GetSupplierByIdRequest request)
    {
        /*Console.WriteLine($"Requested ID: {request.SupplierId}");
        Console.WriteLine($"Request null? {request == null}");

        if (request != null)
        {
            Console.WriteLine($"Requested ID: {request.SupplierId}");
        }*/
        var supplier = _context.Suppliers
            .FirstOrDefault(s => s.SupplierId == request.SupplierId);
        //Console.WriteLine("========== SUPPLIERS ==========");
        /*foreach (var s in _context.Suppliers.ToList())
        {
            Console.WriteLine(
                $"ID={s.SupplierId}, Name={s.Name}");
        }*/
        if (supplier == null)
        {
            //Console.WriteLine($"supplier == null | Requested ID: {request.SupplierId}");
            throw new FaultException<SupplierNotFoundFault>(
                new SupplierNotFoundFault
                {
                    Message = "Supplier not found"
                });
        }
        //Console.WriteLine("==============================");
        return new GetSupplierByIdResponse
        {
            SupplierId = supplier.SupplierId,
            Name = supplier.Name
        };
    }

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

        return new CreatePurchaseOrderResponse
        {
            PurchaseOrderId = order.PurchaseOrderId,
            Message = "Purchase order created successfully"
        };
    }
    
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

        return new UpdatePurchaseOrderStatusResponse
        {
            Message = "Order status updated successfully"
        };
    }
}