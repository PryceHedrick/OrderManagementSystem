using System;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Models
{
    public class Billing
    {
        [Key]
        public string BillingId { get; set; } 

        [Required]
        public string BillingAccountId { get; set; } 

        public double Amount { get; set; }

        public DateTime DateCreated { get; set; }

        // Navigation property
        public BillingAccounts BillingAccount { get; set; }
    }
}