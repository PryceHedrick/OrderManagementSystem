using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Models
{
    public class CostBasedBilling
    {
        [Required]
        public string BillingAccountId { get; set; } 

        [Required]
        public string CostChargeId { get; set; } 

        // Navigation Properties
        public BillingAccounts BillingAccount { get; set; }
        public CostBasedCharge CostBasedCharge { get; set; }
    }
}