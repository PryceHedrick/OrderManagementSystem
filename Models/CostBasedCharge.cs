using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Models
{
    public class CostBasedCharge
    {
        [Key]
        [Required]
        public string CostChargeId { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Amount { get; set; } 
    }
}