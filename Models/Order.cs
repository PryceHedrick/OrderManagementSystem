using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    public class Order
    {
        [Key]
        [Column("Order_ID")]
        [StringLength(25)]
        public string OrderId { get; set; }

        [Required]
        [Column("Customer_ID")]
        [StringLength(25)]
        public string CustomerId { get; set; }

        [Required]
        [Column("User_ID")]
        [StringLength(25)]
        public string UserId { get; set; }

        [Column("Order_Date")]
        public DateTime OrderDate { get; set; }

        [Column("Shipped_Date")]
        public DateTime? ShippedDate { get; set; }

        [Column("Order_Status")]
        [StringLength(25)]
        public string OrderStatus { get; set; }

        [Column("Total_Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<OrderBasedCharge> OrderBasedCharges { get; set; } = new List<OrderBasedCharge>();
    }
}