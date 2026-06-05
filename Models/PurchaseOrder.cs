namespace SoapApi.Models
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public string Status { get; set; } = "Pending";

        public decimal TotalAmount { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<PurchaseOrderLine> Lines { get; set; }
            = new List<PurchaseOrderLine>();
    }
}
