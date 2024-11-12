using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Models
{
    public class OrderBasedBilling
    {
        [Required]
        public string BillingAccountId { get; set; } 

        [Required]
        public string OrderChargeId { get; set; } 

        // Navigation Properties
        public BillingAccount BillingAccount { get; set; }
        public OrderBasedCharge OrderBasedCharge { get; set; }
    }
}