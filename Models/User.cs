using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Models
{
    public class User
    {
        [Key]
        [StringLength(25)]
        public string UserId { get; set; }

        [Required]
        [StringLength(25)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; } 

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        public DateTime DateCreated { get; set; }

        // Navigation Properties
        public ICollection<BillingAccounts> BillingAccounts { get; set; } = new List<BillingAccounts>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<Order> Orders { get; set; } = new List<Order>(); 
    }
}