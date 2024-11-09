using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    public class Billing
    {
        [Key]
        [Column("Billing_ID")]
        [StringLength(25)]
        public string BillingId { get; set; }

        [Required]
        [Column("Billing_Account_ID")]
        [StringLength(25)]
        public string BillingAccountId { get; set; }

        [Required]
        [Column("Charge_ID")]
        [StringLength(25)]
        public string ChargeId { get; set; }

        [Column("Amount", TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Column("Date_Created")]
        public DateTime DateCreated { get; set; }

        // Navigation Properties
        public BillingAccounts BillingAccount { get; set; }
        public Charge Charge { get; set; }
    }
}