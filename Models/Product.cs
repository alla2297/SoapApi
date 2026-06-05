using System.ComponentModel.DataAnnotations;
namespace SoapApi.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int StockQuantity { get; set; }
    }
}
