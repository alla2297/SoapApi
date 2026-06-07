using SoapApi.Data;
using SoapApi.Models;

namespace SoapApi.Services;

public class AuditService
{
    private readonly AppDbContext _context;

    public AuditService(AppDbContext context)
    {
        _context = context;
    }

    public void LogCreatePurchaseOrder(PurchaseOrder order)
    {
        _context.AuditLogs.Add(new AuditLog
        {
            Action = "CREATE",
            EntityName = "PurchaseOrder",
            EntityId = order.PurchaseOrderId,
            Details = $"Created order for supplier {order.SupplierId}",
            CreatedAt = DateTime.UtcNow
        });

        _context.SaveChanges();
    }

    public void LogUpdatePurchaseOrderStatus(
        PurchaseOrder order,
        string oldStatus)
    {
        _context.AuditLogs.Add(new AuditLog
        {
            Action = "UPDATE",
            EntityName = "PurchaseOrder",
            EntityId = order.PurchaseOrderId,
            Details =
                $"Status changed from {oldStatus} to {order.Status}",
            CreatedAt = DateTime.UtcNow
        });

        _context.SaveChanges();
    }
}