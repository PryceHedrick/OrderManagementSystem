using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    public class OrderBasedCharge
    {
        [Key]
        [Column("Order_Based_Charge_ID")]
        [StringLength(25)]
        public string OrderBasedChargeId { get; set; }

        [Required]
        [Column("Order_ID")]
        [StringLength(25)]
        public string OrderId { get; set; }

        [Required]
        [Column("Charge_ID")]
        [StringLength(25)]
        public string ChargeId { get; set; }

        [Column("Amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Column("Date_Charged")]
        public DateTime DateCharged { get; set; }

        // Navigation Properties
        public Order Order { get; set; }
        public Charge Charge { get; set; }
    }
}