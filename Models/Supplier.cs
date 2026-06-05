using System.ComponentModel.DataAnnotations;
namespace SoapApi.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
            = new List<PurchaseOrder>();
    }
}
