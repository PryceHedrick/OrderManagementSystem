using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    public class Inventory
    {
        [Key]
        [Column("Product_ID")]
        [StringLength(25)]
        public string ProductId { get; set; }

        [Required]
        [Column("Warehouse_ID")]
        [StringLength(25)]
        public string WarehouseId { get; set; }

        [Column("SKU")]
        [StringLength(50)]
        public string SKU { get; set; }

        [Column("Product_Name")]
        [StringLength(255)]
        public string ProductName { get; set; }

        [Column("Product_Description")]
        [StringLength(255)]
        public string ProductDescription { get; set; }

        // Navigation Properties
        public Warehouse Warehouse { get; set; }
        public ICollection<InboundProductList> InboundProductLists { get; set; } = new List<InboundProductList>();
        public ICollection<FreightProductList> FreightProductLists { get; set; } = new List<FreightProductList>();
        public ICollection<ParcelProductList> ParcelProductLists { get; set; } = new List<ParcelProductList>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<PlatformProductList> PlatformProductLists { get; set; } = new List<PlatformProductList>();
    }
}