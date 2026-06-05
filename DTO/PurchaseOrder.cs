namespace SoapApi.DTO
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }

        public int SupplierId { get; set; } //FK to Ordre
        public int Amount { get; set; }
        public enum Status { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
