using Microsoft.EntityFrameworkCore;
using SoapApi.Models;

namespace SoapApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Supplier> Suppliers { get; set; }

    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<AuditLog> AuditLogs { get; set; }
}
