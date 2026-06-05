namespace SoapApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
