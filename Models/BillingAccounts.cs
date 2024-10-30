using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Models
{
    public class BillingAccounts
    {
        [Key]
        public string BillingAccountId { get; set; } 

        [Required]
        public string UserId { get; set; } 

        public double AccountBalance { get; set; } 

        // Navigation properties
        public User User { get; set; }

        public ICollection<Billing> Billings { get; set; } = new List<Billing>();
        public ICollection<CostBasedBilling> CostBasedBillings { get; set; } = new List<CostBasedBilling>();
        public ICollection<OrderBasedBilling> OrderBasedBillings { get; set; } = new List<OrderBasedBilling>();
    }
}