using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    public class PlatformProductList
    {
        [Required]
        [Column("Order_ID")]
        [StringLength(25)]
        public string OrderId { get; set; }

        [Required]
        [Column("Product_ID")]
        [StringLength(25)]
        public string ProductId { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        // Navigation Properties
        public PlatformOrder PlatformOrder { get; set; }
        public Inventory Inventory { get; set; }
    }
}
