using Microsoft.EntityFrameworkCore;
using SoapApi.Models;
using System.Data;

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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // =========================
        // Making TotalAmoun to two dicimal
        // =========================
        modelBuilder.Entity<PurchaseOrder>()
            .Property(p => p.TotalAmount)
            .HasPrecision(10, 2);
        // =========================
        // Seed Supplies
        // =========================
        modelBuilder.Entity<Supplier>().HasData(
            new Supplier
            {
                SupplierId = 1,
                Name = "ABC Supplies",
                Email = "abc@test.com",
                Phone = "12345678",
                CreatedAt = new DateTime(2026, 6, 6)
            }
        );
    }
}
