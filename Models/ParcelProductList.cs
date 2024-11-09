using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    public class ParcelProductList
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

       
        public ParcelOutbound ParcelOutbound { get; set; }
        public Inventory Inventory { get; set; }
    }
}
